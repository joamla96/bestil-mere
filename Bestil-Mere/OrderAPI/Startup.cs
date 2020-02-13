using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using OrderAPI.Db;
using OrderAPI.Extensions;
using OrderAPI.Hubs;
using OrderAPI.Messaging;
using OrderAPI.Models;
using OrderAPI.Services;
using StackExchange.Redis;

namespace OrderAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:4200", "http://localhost:5021", "http://gateway")
                    .AllowCredentials();
            }));
            // requires using Microsoft.Extensions.Options
            services.Configure<OrderDatabaseSettings>(
                Configuration.GetSection(nameof(OrderDatabaseSettings)));

            services.AddSingleton<IOrderDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<OrderDatabaseSettings>>().Value);
            services.AddSingleton<MongoDbManager>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddSingleton<MessagePublisher>();
            services.AddSingleton<MessageListener>();
            services.Configure<MessagingSettings>(Configuration.GetSection(nameof(MessagingSettings)));
            services.AddSingleton<IMessagingSettings>(sp => sp.GetRequiredService<IOptions<MessagingSettings>>().Value);
            services.AddSignalR().AddStackExchangeRedis("redis:6379", options =>
            {
                options.Configuration.Password = "redis";
                options.Configuration.ChannelPrefix = "orderapi";
                options.ConnectionFactory = async writer =>
                {
                    var config = new ConfigurationOptions
                    {
                        AbortOnConnectFail = false
                    };
                    config.EndPoints.Add(IPAddress.Loopback, 0);
                    config.SetDefaultPorts();
                    var connection = await ConnectionMultiplexer.ConnectAsync(config, writer);
                    connection.ConnectionFailed += (_, e) => { Console.WriteLine("Connection to Redis failed."); };

                    if (!connection.IsConnected)
                    {
                        Console.WriteLine("Did not connect to Redis.");
                    }

                    return connection;
                };
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseMessageListener();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<OrderHub>("/order-updates");
            });
        }
    }
}