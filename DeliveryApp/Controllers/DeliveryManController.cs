using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Models.Data;
using DeliveryApp.Models.ViewModels;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class DeliveryManController : Controller
    {
        private readonly IDeliveryManService deliveryManService;
        private readonly ILocationService locationService;

        public DeliveryManController(IDeliveryManService deliveryManService, ILocationService locationService)
        {
            this.deliveryManService = deliveryManService;
            this.locationService = locationService;
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

        [HttpGet]
        public IActionResult EditDeliveryMan(int id)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(id);
            DeliveryMenViewModel model = new DeliveryMenViewModel { DeliveryMan = deliveryMan };

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult EditDeliveryMan(DeliveryMenViewModel dvm)
        {
            var deliveryManLocation = locationService.GetLocationById(dvm.DeliveryMan.Location.Id);
            var editedEntity = new DeliveryMan
            {
                Id = dvm.DeliveryMan.Id,
                FirstName = dvm.DeliveryMan.FirstName,
                LastName = dvm.DeliveryMan.LastName,
                Location = deliveryManLocation,
                Phone = dvm.DeliveryMan.Phone,
                Email = dvm.DeliveryMan.Email,
                IsAvailable = dvm.DeliveryMan.IsAvailable,
                IsValidated = true,
                PicturePath = dvm.DeliveryMan.PicturePath
            };
            deliveryManService.EditDeliveryMan(editedEntity);

            TempData["Message"] = "Livreur modifié avec succès !";

            return RedirectToAction("AllDeliveryMen");
        }
    }
}