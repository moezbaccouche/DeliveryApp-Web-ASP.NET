using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO.OrdersDTOs
{
    public class ClientPendingOrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public EnumOrderStatus OrderStatus { get; set; }
        public double OrderPrice { get; set; }
        public double DeliveryPrice { get; set; }
    }
}
