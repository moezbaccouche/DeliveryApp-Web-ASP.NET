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
        Order DeleteOrder(int id);
        Order EditOrder(Order order);
        Order GetClientPendingOrder(int clientId);
        IEnumerable<Order> GetClientTreatedOrders(int clientId);
        IEnumerable<Order> GetClientOrders(int clientId);
        int GetClientNbDeliveredProducts(int clientId);
        IEnumerable<Order> GetAllPendingOrders();
        IEnumerable<Order> GetAllDeliveredOrders();
        IEnumerable<Order> GetAllProcessingOrders();
        IEnumerable<Order> GetAllInDeliveryOrders();
    }
}
