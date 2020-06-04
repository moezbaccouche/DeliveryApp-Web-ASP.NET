using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.DTO
{
    public class ProcessingOrderDto
    {
        public Order ProcessingOrder { get; set; }
        public Client Client { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
        public DeliveryInfo DeliveryInfo { get; set; }
    }
}
