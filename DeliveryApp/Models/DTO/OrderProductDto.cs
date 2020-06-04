using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.DTO
{
    public class OrderProductDto
    {
        public Product Product { get; set; }
        public ProductImage ProductImage { get; set; }
        public ProductOrder OrderProduct { get; set; }
    }
}
