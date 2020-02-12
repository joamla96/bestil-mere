using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using OrderAPI.Db;
using OrderAPI.Extensions;
using OrderAPI.Messaging;
using OrderAPI.Models;
using OrderAPI.Services;

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
            
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod(); 
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            //corsBuilder.WithOrigins("http://gateway"); // for a specific url. Don't add a forward slash on the end!
            

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", corsBuilder.Build());
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}