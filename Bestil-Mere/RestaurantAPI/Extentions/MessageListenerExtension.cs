using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantAPI.Messaging;
using RestaurantAPI.Services;

namespace RestaurantAPI.Extensions
{
    public static class MessageListenerExtension
    {
        //the simplest way to store a single long-living object, just for example.
        private static MessageListener Listener { get; set; }

        public static IApplicationBuilder UseMessageListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<MessageListener>();

            var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            lifetime.ApplicationStarted.Register(OnStarted);

            lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            Listener.Register();
        }

        private static void OnStopping()
        {
            Listener.Unregister();
        }
    }
}