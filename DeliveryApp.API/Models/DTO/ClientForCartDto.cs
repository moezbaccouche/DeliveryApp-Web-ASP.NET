using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class ClientForCartDto
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
    }
}
