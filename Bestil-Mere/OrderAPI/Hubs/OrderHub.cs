using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace OrderAPI.Hubs
{
    public class OrderHub : Hub
    {
        public static readonly ConcurrentDictionary<string, string> Connections = new ConcurrentDictionary<string, string>();
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected!");
            var orderId = Context.GetHttpContext().Request.Query["order"];
            Console.WriteLine($"Client has orderid: {orderId}");
            Connections.TryAdd(orderId, Context.ConnectionId);
            Context.Items.Add(orderId, Context.ConnectionId);
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