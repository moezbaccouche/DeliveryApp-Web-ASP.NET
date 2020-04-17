using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class ProductForOrderCreationDto
    {
        public int ProductId { get; set; }
        public string Amount { get; set; }
    }
}
