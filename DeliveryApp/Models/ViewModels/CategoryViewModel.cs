using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public string Message { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
