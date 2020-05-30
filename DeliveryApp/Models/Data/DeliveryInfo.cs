using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class DeliveryInfo : BaseEntity
    {
        public DateTime AcceptingOrderTime { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime RealDeliveryTime { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
        public int IdDeliveryMan { get; set; }
        public int IdOrder { get; set; }
        public Order Order { get; set; }
        public byte[] SignatureImageBase64 { get; set; }
        public string SignatureImagePath { get; set; }
    }
}
