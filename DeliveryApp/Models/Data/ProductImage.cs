using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class ProductImage : BaseEntity
    {
        public Product Product { get; set; }
        public string ImagePath { get; set; }
        public byte[] ImageBase64 { get; set; }
    }
}
