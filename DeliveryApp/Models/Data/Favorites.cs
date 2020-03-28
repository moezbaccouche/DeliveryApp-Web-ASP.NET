using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class Favorites : BaseEntity
    {
        public Product Product { get; set; }
        public Client Client { get; set; }
    }
}
