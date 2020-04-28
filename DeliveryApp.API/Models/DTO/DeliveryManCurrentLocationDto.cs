using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class DeliveryManCurrentLocationDto
    {
        public double Long { get; set; }
        public double Lat { get; set; }
        public int DeliveryManId { get; set; }
    }
}
