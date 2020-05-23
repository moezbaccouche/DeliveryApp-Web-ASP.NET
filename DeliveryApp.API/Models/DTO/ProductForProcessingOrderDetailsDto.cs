using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class ProductForProcessingOrderDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageBase64 { get; set; }
        public string Amount { get; set; }
        public bool NotBought { get; set; }
    }
}
