using System;
using LogisticsAPI.Extensions;
using LogisticsAPI.Messaging;
using LogisticsAPI.Models;
using LogisticsAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace LogisticsAPI
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
            services.Configure<LogisticsDatabaseSettings>(
                Configuration.GetSection(nameof(LogisticsDatabaseSettings)));

            // Configure messaging settings
            services.Configure<MessagingSettings>(
                Configuration.GetSection(nameof(MessagingSettings)));
            services.AddSingleton<IMessagingSettings>(sp => 
                sp.GetRequiredService<IOptions<MessagingSettings>>().Value);

            services.AddSingleton<ILogisticsDatabaseSettings>(sp => 
                sp.GetRequiredService<IOptions<LogisticsDatabaseSettings>>().Value);
            services.AddSingleton<MongoDbService>();
            services.AddSingleton<MessageListener>();
            services.AddSingleton<MessagePublisher>();
            
            services.AddTransient<ILogisticsPartnerService, LogisticPartnerService>();
            services.AddTransient<IDeliveryService, DeliveryService>();
            
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Logistics API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMessageListener(); // Listens for messages for order requests

            app.UseSwagger();
            
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Logistics API V1");
            });
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}