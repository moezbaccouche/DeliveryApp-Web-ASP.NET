using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class Order : BaseEntity
    {
        public EnumOrderStatus Status { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime RealDeliveryTime { get; set; }
        public double OrderPrice { get; set; }
        public double DeliveryPrice { get; set; }
        public Client Client { get; set; }
        public int IdClient { get; set; }
        public DeliveryMan DeliveryMan { get; set; }

        [NotMapped]
        public ICollection<Product> Products { get; set; }
    }
}
