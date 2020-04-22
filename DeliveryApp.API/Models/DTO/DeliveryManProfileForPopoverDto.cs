using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class DeliveryManProfileForPopoverDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public byte[] ImageBase64 { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Rating { get; set; }
        public int NbRatings{ get; set; }
    }
}
