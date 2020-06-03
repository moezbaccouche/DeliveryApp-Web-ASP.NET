using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int NbPendingOrders { get; set; }
        public int NbDeliveredOrders { get; set; }
        public int NbClients { get; set; }
        public int NbNewDeliveryMen { get; set; }
    }
}
