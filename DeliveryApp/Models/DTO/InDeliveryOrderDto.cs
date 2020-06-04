using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.DTO
{
    public class InDeliveryOrderDto
    {
        public Order InDeliveryOrder { get; set; }
        public Client Client { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
        public DeliveryInfo DeliveryInfo { get; set; }
        public CurrentLocation DeliveryManCurrentLocation { get; set; }
    }
}
