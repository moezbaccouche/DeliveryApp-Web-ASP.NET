using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class DeliveryInfoForTrackingDto
    {
        public int OrderId { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public double Distance { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime AcceptingDeliveryTime { get; set; }
        public DateTime RealDeliveryTime { get; set; }
        public DeliveryManForTrackingPageDto DeliveryMan { get; set; }
    }
}
