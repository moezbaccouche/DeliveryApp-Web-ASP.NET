using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class ProductForHomeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public byte[] ImageBase64 { get; set; }
    }
}
