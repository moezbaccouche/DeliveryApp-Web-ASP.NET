using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class BoughtProductDto
    {
        public int IdProduct { get; set; }
        public int IdOrder { get; set; }
        public int BoughtAmount { get; set; }
    }
}
