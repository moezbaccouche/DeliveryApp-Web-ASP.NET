using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class PendingOrdersForDeliveryManDto
    {
        public int OrderId { get; set; }
        public ClientForPendingOrdersDto Client { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
