using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class Client : BaseEntity
    {
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PicturePath { get; set; }
        public byte[] ImageBase64 { get; set; }
        public bool HasValidatedEmail { get; set; }
        public Location Location { get; set; }
        public string PlayerId { get; set; }
    }
}
