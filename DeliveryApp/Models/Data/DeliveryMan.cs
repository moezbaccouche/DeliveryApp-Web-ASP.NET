using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class DeliveryMan : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Location Location { get; set; }
        public bool IsValidated { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<Order> OrdersToDeliver { get; set; }
    }
}
