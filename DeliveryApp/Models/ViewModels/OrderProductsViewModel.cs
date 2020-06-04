using DeliveryApp.Models.Data;
using DeliveryApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class OrderProductsViewModel
    {
        //public IEnumerable<ProductOrder> OrderProducts { get; set; }
        //public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<OrderProductDto> OrderProducts { get; set; }
    }
}
