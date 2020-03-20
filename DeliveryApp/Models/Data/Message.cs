using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class Message : BaseEntity
    {
        public Client Client { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageTime { get; set; }
    }
}
