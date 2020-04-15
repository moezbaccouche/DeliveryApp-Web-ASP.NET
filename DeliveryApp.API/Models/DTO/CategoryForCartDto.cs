using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class CategoryForCartDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NbProducts { get; set; } = 0;
    }
}
