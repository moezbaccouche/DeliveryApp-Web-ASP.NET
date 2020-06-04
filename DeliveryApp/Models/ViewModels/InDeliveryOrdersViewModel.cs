using DeliveryApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class InDeliveryOrdersViewModel
    {
        public IEnumerable<InDeliveryOrderDto> InDeliveryOrders { get; set; }
    }
}
