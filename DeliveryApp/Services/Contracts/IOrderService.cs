using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface IOrderService
    {
        Order AddOrder(Order order);
        Order GetOrderById(int id);
        IEnumerable<Order> GetDeliveredOrders();
        IEnumerable<Order> GetNotDeliveredOrders();
        IEnumerable<Order> GetInDeliveryOrders();
        Order DeleteOrder(int id);
        Order EditOrder(Order order);
        Order BindOrder(Order order, DeliveryMan deliveryMan);
    }
}
