using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class ProductOrder : BaseEntity
    {
        public Order Order { get; set; }
        public Product Article { get; set; }
        public string Amount { get; set; }
    }
}
