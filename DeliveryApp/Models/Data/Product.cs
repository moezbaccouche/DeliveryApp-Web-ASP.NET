using DeliveryApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public EnumProductUnit ProductUnit { get; set; }
        public int CategoryId { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
