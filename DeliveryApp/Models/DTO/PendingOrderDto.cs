using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.DTO
{
    public class PendingOrderDto
    {
        public Order PendingOrder { get; set; }
        public Client Client { get; set; }
    }
}
