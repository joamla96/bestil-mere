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
using PaymentAPI.Db;
using PaymentAPI.Extensions;
using PaymentAPI.Messaging;
using PaymentAPI.Services;

namespace PaymentAPI
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
            // Configure database settings
            services.Configure<PaymentDatabaseSettings>(
                Configuration.GetSection(nameof(PaymentDatabaseSettings)));
            services.AddSingleton<IPaymentDatabaseSettings>(sp => 
                sp.GetRequiredService<IOptions<PaymentDatabaseSettings>>().Value);

            // Configure messaging settings
            services.Configure<MessagingSettings>(
                Configuration.GetSection(nameof(MessagingSettings)));
            services.AddSingleton<IMessagingSettings>(sp => 
                sp.GetRequiredService<IOptions<MessagingSettings>>().Value);

            // Add classes to DI container
            services.AddSingleton<MongoDbManager>();
            services.AddSingleton<MessageListener>();
            services.AddSingleton<MessagePublisher>();
            services.AddTransient<IPaymentService, PaymentService>();
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMessageListener(); // Listens for messages for payment requests

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}