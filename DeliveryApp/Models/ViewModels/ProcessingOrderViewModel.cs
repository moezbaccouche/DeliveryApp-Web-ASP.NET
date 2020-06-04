using DeliveryApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class ProcessingOrderViewModel
    {
        public IEnumerable<ProcessingOrderDto> ProcessingOrders { get; set; }
    }
}
