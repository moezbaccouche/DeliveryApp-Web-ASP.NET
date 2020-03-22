using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Models.ViewModels;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllClients()
        {
            var allClients = clientService.GetAllClients();
            ClientViewModel cvm = new ClientViewModel { AllClients = allClients };

            return View(cvm);
        }
    }
}