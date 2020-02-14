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
        public static readonly ConcurrentDictionary<string, string> Connections = new ConcurrentDictionary<string, string>();
        private readonly IOrderService _orderService;

        public OrderHub(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected!");
            var orderId = Context.GetHttpContext().Request.Query["order"];
            Console.WriteLine($"Client has orderid: {orderId}");
            if (Connections.ContainsKey(orderId))
            {
                Connections.TryUpdate(orderId, Context.ConnectionId, StringComparison.Ordinal.ToString());
            }
            Connections.TryAdd(orderId, Context.ConnectionId);
            Context.Items.Add(orderId, Context.ConnectionId);
            var order = _orderService.Get(orderId);
            Clients.Client(Context.ConnectionId).SendAsync("orderUpdates", order.Result.OrderStatus);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"Client disconnected!");
            var orderId = Context.GetHttpContext().Request.Query["order"];
            Console.WriteLine($"Client has orderid: {orderId}");
            Connections.TryRemove(orderId, out var s);
            return base.OnDisconnectedAsync(exception);
        }
        
    }
}