using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Models.ViewModels;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class DeliveryManController : Controller
    {
        private readonly IDeliveryManService deliveryManService;

        public DeliveryManController(IDeliveryManService deliveryManService)
        {
            this.deliveryManService = deliveryManService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllDeliveryMen()
        {
            var allDeliveryMen = deliveryManService.GetAllDeliveryMen();
            DeliveryMenViewModel deliveryMenViewModel = new DeliveryMenViewModel
            { 
                AllDeliveryMen = allDeliveryMen
            };
            return View(deliveryMenViewModel);
        }

        public IActionResult NotValidatedDeliveryMen()
        {
            var notValidatedDeliveryMen = deliveryManService.GetNotValidatedDeliveryMen();
            DeliveryMenViewModel deliveryMenViewModel = new DeliveryMenViewModel
            {
                NotValidatedDeliveryMen = notValidatedDeliveryMen
            };
            return View(deliveryMenViewModel);
        }

        public IActionResult Maps()
        {
            return View();
        }
    }
}