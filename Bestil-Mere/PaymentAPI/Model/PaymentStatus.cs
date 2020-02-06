using System;
using Models.Payment;

namespace PaymentAPI.Model
{
    public enum PaymentStatus
    {
        Created,
        Authorizing,
        Accepted,
        Captured
    }

    public static class PaymentStatusExtensions
    {
        public static PaymentStatusDTO Parse(this PaymentStatus p)
        {
            return Enum.Parse<PaymentStatusDTO>(p.ToString());
        }
    }
}