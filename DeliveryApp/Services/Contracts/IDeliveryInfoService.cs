using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface IDeliveryInfoService
    {
        DeliveryInfo AddDeliveryInfo(DeliveryInfo newDeliveryInfo);
        DeliveryInfo EditDeliveryInfo(DeliveryInfo newDeliveryInfo);
        DeliveryInfo GetOrderDeliveryInfo(int orderId);
        IEnumerable<DeliveryInfo> GetAllDeliveryInfos();
        IEnumerable<DeliveryInfo> GetDeliveryManOrderHistory(int deliveryManId);
    }
}
