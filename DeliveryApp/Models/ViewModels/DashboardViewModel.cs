using DeliveryApp.Models.Data;
using DeliveryApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int NbPendingOrders { get; set; }
        public IEnumerable<Order> DeliveredOrders { get; set; }
        public int NbClients { get; set; }
        public int NbNewDeliveryMen { get; set; }
        public IEnumerable<RatedDeliveryManDto> RatedDeliveryMen { get; set; }
        public IEnumerable<DeliveredOrdersPerDeliveryManDto> DeliveredOrdersPerDeliveryMan { get; set; }
    }
}
