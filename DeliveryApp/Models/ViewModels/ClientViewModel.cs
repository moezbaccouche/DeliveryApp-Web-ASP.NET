using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class ClientViewModel
    {
        public IEnumerable<Client> AllClients { get; set; }
    }
}
