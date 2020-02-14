using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OrderAPI.Models;
using OrderAPI.Services;

namespace OrderAPI.Hubs
{
    public class OrderHub : Hub
    {
        private readonly OrderConnections _connections;
        private readonly IOrderService _orderService;

        public OrderHub(IOrderService orderService, OrderConnections orderConnections)
        {
            _orderService = orderService;
            _connections = orderConnections;
        }
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected!");
            var orderId = Context.GetHttpContext().Request.Query["order"];
            Console.WriteLine($"Client has orderid: {orderId}");
            _connections.SetConnectionIdAsync(orderId, Context.ConnectionId).Wait();
            
            var order = _orderService.Get(orderId).Result;
            Clients.Client(Context.ConnectionId).SendAsync("orderUpdates", order.OrderStatus);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"Client disconnected!");
            var orderId = Context.GetHttpContext().Request.Query["order"];
            Console.WriteLine($"Client has orderid: {orderId}");
            _connections.RemoveAsync(orderId).Wait();
            return base.OnDisconnectedAsync(exception);
        }
        
    }
}