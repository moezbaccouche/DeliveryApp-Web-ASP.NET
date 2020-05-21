using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class ProcessingOrderDetailsDto
    {
        public int Id { get; set; }
        public string StatusString { get; set; }
        public EnumOrderStatus Status { get; set; }
        public double OrderPrice { get; set; }
        public IEnumerable<ProductForCheckout> Products { get; set; }
        public ClientForPendingOrdersDto Client { get; set; }
    }
}
