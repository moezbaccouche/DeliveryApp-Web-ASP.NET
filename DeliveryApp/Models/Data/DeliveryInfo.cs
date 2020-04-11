using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class DeliveryInfo : BaseEntity
    {
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime RealDeliveryTime { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
        public Order Order { get; set; }
    }
}
