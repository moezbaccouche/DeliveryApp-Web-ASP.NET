using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class OrderProductsViewModel
    {
        public IEnumerable<ProductOrder> OrderProducts { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
    }
}
