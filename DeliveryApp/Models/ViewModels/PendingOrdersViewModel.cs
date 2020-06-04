using DeliveryApp.Models.Data;
using DeliveryApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class PendingOrdersViewModel
    {
        public IEnumerable<PendingOrderDto> PendingOrders { get; set; }
        public IEnumerable<DeliveryMan> AllDeliveryMen { get; set; }
    }
}
