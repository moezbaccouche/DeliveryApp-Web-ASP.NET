using DeliveryApp.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class DeliveryMenViewModel
    {
        public IEnumerable<DeliveryMan> NotValidatedDeliveryMen { get; set; }

        public IEnumerable<DeliveryMan> AllDeliveryMen { get; set; }
        public DeliveryMan DeliveryMan { get; set; }

        [TempData]
        public string Message { get; set; }
    }
}
