using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IClientService clientService;

        public AccountController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public IActionResult ConfirmClientEmail(string userId, string code)
        {
            var client = clientService.GetClientByIdentityId(userId);
            client.HasValidatedEmail = true;
            clientService.UpdateClient(client);

            return View();
        }
    }
}