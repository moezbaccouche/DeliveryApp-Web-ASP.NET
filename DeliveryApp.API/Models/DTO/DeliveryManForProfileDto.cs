using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class DeliveryManForProfileDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Location Location { get; set; }
        public double Rating { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] ImageBase64 { get; set; }
    }
}
