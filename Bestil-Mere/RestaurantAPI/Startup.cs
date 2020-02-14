using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestaurantAPI.Db;
using RestaurantAPI.Extensions;
using RestaurantAPI.Messaging;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI
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
            services.Configure<RestaurantDatabaseSettings>(
                Configuration.GetSection(nameof(RestaurantDatabaseSettings)));

            // Configure messaging settings
            services.Configure<MessagingSettings>(
                Configuration.GetSection(nameof(MessagingSettings)));
            services.AddSingleton<IMessagingSettings>(sp => 
                sp.GetRequiredService<IOptions<MessagingSettings>>().Value);
            
            services.AddSingleton<IRestaurantDatabaseSettings>(sp => 
                sp.GetRequiredService<IOptions<RestaurantDatabaseSettings>>().Value);
            services.AddSingleton<MongoDbManager>();
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddSingleton<MessageListener>();
            services.AddSingleton<MessagePublisher>();
            services.AddTransient<IMenuService, MenuService>();
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMessageListener(); // Listens for messages for order requests

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}