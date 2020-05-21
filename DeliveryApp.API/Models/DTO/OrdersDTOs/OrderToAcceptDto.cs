using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO.OrdersDTOs
{
    public class OrderToAcceptDto
    {
        public int OrderId { get; set; }
        public int DeliveryManId { get; set; }
        public double DurationToDestination { get; set; }
    }
}
