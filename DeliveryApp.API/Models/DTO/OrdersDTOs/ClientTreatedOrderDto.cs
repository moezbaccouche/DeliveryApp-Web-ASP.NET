using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO.OrdersDTOs
{
    public class ClientTreatedOrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public double OrderPrice { get; set; }
        public double DeliveryPrice { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public int DeliveryManId { get; set; }
        public string DeliveryManName { get; set; }
        public byte[] DeliveryManPicture { get; set; }
        public EnumOrderStatus OrderStatus { get; set; }
        public DateTime RealDeliveryTime { get; set; }
    }
}
