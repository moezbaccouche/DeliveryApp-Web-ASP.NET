using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class CurrentLocation : BaseEntity
    {
        public double Long { get; set; }
        public double Lat { get; set; }
        public int DeliveryManId { get; set; }
    }
}
