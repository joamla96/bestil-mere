using Models.Messages.Logistics;

namespace LogisticsAPI.Services
{
    public interface IDeliveryService
    {
        public void DeliveryRequest(DeliveryRequest dr);
    }
}