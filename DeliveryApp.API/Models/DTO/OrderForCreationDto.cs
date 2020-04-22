using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class OrderForCreationDto
    {
        //public IEnumerable<ProductForOrderCreationDto> Products { get; set; }
        public int ClientId { get; set; }
        public bool WithBill { get; set; }
    }
}
