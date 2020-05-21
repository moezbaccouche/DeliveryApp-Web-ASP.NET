using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class PendingOrderDetailsDto
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public double OrderPrice { get; set; }
        public IEnumerable<ProductForCheckout> Products { get; set; }
        public double DeliveryPrice { get; set; }
        public ClientForPendingOrdersDto Client { get; set; }

    }
}
