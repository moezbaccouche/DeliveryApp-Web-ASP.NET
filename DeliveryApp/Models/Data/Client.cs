using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class Client : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PicturePath { get; set; }
        public byte[] ImageBase64 { get; set; }
        public Location Location { get; set; }
        public ICollection<Order> Orders { get; set; } 
            = new List<Order>();
        
    }
}
