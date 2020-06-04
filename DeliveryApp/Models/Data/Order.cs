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
        public double OrderPrice { get; set; }
        public double DeliveryPrice { get; set; }
        public int IdClient { get; set; }
        public bool WithBill { get; set; }
    }
}
