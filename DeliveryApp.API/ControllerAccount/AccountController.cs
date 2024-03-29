﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Models.ViewModels;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IClientService clientService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDeliveryManService deliveryManService;

        public AccountController(IClientService clientService, UserManager<IdentityUser> userManager,
            IDeliveryManService deliveryManService)
        {
            this.clientService = clientService;
            _userManager = userManager;
            this.deliveryManService = deliveryManService;
        }

        
        [HttpGet("api/ConfirmClientEmail")]
        public IActionResult ConfirmClientEmail(string userId, string code)
        {
            var client = clientService.GetClientByIdentityId(userId);
            if (!client.HasValidatedEmail)
            {
                client.HasValidatedEmail = true;
                clientService.UpdateClient(client);

                return View("ConfirmUserEmail");
            }
            return View("NotFound");
        }

        [HttpGet("api/ConfirmDeliveryManEmail")]
        public IActionResult ConfirmDeliveryManEmail(string userId, string code)
        {
            var deliveryMan = deliveryManService.GetDeliveryManByIdentityId(userId);
            if (!deliveryMan.HasValidatedEmail)
            {
                deliveryMan.HasValidatedEmail = true;
                deliveryManService.EditDeliveryMan(deliveryMan);

                return View("ConfirmUserEmail");
            }
            return View("NotFound");
        }

        [HttpGet("api/resetPassword")]
        public IActionResult ResetPassword(string userId, string token)
        {
            var newToken = token.Replace(" ", "+");
            var model = new ResetPasswordViewModel { IdentityId = userId, Token = newToken };

            return View(model);
        }

        [HttpPost("api/resetPassword")]
        public async Task<IActionResult> ResetPassword([Bind("Password, RepeatedPassword")]ResetPasswordViewModel model, string userId, string token)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (result.Succeeded)
                {
                    return View("SuccessfulPasswordReset");
                }
                return View("FailedPasswordReset");
            }
            return View(model);
        }


    }
}