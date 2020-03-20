using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class Bill : BaseEntity
    {
        public Order Order { get; set; }
    }
}
