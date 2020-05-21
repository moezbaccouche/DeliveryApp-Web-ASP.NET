using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class OrdersHistoryForDeliveryManDto
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime RealDeliveryTime { get; set; }
        public double OrderPrice { get; set; }
        public double DeliveryPrice { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public byte[] ClientPicture { get; set; }
    }
}
