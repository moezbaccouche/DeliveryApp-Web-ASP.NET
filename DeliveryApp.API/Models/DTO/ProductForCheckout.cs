using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class ProductForCheckout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageBase64 { get; set; }
        public double TotalProductPrice { get; set; }
        public string Amount { get; set; }
        public string Category { get; set; }
    }
}
