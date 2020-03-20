using DeliveryApp.Models.Data;
using DeliveryApp.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<SelectListItem> Units { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> AllProducts { get; set; }
    }
}
