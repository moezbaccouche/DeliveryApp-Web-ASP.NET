using DeliveryApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class DeliveredOrdersViewModel
    {
        public IEnumerable<DeliveredOrderDto> DeliveredOrders { get; set; }
    }
}
