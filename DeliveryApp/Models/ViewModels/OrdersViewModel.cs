using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class OrdersViewModel
    {
        public IEnumerable<Order> DeliveredOrders { get; set; }
        public IEnumerable<Order> InDeliveryOrders { get; set; }
        public IEnumerable<Order> NotDeliveredOrders { get; set; }
    }
}
