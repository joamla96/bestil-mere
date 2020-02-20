using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Hubs
{
    public class RestaurantHub : Hub
    {
        private readonly RestaurantConnections _connections;
        private readonly IRestaurantService _restaurantService;

        public RestaurantHub(IRestaurantService restaurantService, RestaurantConnections restaurantConnections)
        {
            _restaurantService = restaurantService;
            _connections = restaurantConnections;
        }
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected!");
            var restaurantId = Context.GetHttpContext().Request.Query["restaurant"];
            Console.WriteLine($"Client has restaurantid: {restaurantId}");
            _connections.SetConnectionIdAsync(restaurantId, Context.ConnectionId).Wait();
            
            //var restaurant = _restaurantService.Get(restaurantId).Result;
            //Clients.Client(Context.ConnectionId).SendAsync("restaurantUpdates", restaurant.RestaurantStatus);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"Client disconnected!");
            var restaurantId = Context.GetHttpContext().Request.Query["restaurant"];
            Console.WriteLine($"Client has restaurantid: {restaurantId}");
            _connections.RemoveAsync(restaurantId).Wait();
            return base.OnDisconnectedAsync(exception);
        }
        
    }
}