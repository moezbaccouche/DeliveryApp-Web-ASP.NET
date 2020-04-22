using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class OrderDetailsDto
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime RealDeliveryTime { get; set; }
        public double OrderPrice { get; set; }
        public IEnumerable<ProductForCheckout> Products { get; set; }
        public double DeliveryPrice { get; set; }
        public int DeliveryManId { get; set; }
        public string DeliveryManName { get; set; }
        public byte[] DeliveryManPicture { get; set; }
        public int DeliveryManClientRating { get; set; }
        public EnumOrderStatus OrderStatus { get; set; }
    }
}
