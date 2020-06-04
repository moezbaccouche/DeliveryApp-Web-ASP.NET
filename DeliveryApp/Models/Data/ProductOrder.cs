using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class ProductOrder : BaseEntity
    {
        public int IdProduct { get; set; }
        public int IdOrder { get; set; }
        public string Amount { get; set; }
        public bool NotBought { get; set; }
    }
}
