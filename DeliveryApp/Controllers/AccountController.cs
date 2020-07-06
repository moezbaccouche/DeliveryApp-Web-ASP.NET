using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Extensions;
using DeliveryApp.Models.Data;
using DeliveryApp.Models.ViewModels;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace DeliveryApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IClientService clientService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILocationService locationService;
        private readonly IAdminService adminService;
        private readonly IEmailSenderService emailSenderService;

        public AccountController(IClientService clientService, UserManager<IdentityUser> userManager,
            ILocationService locationService, IAdminService adminService, IEmailSenderService emailSenderService)
        {
            this.clientService = clientService;
            _userManager = userManager;
            this.locationService = locationService;
            this.adminService = adminService;
            this.emailSenderService = emailSenderService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetInt32("AdminId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserCredentialsViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var admin = adminService.GetAdminByIdentityId(user.Id);
                if(admin == null)
                {
                    TempData["ErrorMessage"] = "Email ou mot de passe incorrect.";
                    return View();
                }

                if(!admin.HasValidatedEmail)
                {
                    TempData["ErrorMessage"] = "Votre compte n'est encore activé. Vérifiez votre boite Emails.";
                    return View();
                }
                HttpContext.Session.SetInt32("AdminId", admin.Id);
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Email ou mot de passe incorrect.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("FirstName, LastName, Email, Password, Phone, Address, City, ZipCode")]RegisterViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(await _userManager.FindByEmailAsync(model.Email) != null)
            {
                TempData["ErrorMessage"] = "Email déjà utilisé !";
                return View();
            }

            var result = await _userManager.CreateAsync(new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            }, model.Password);

            if (result.Succeeded)
            {
                var location = locationService.AddLocation(new Location
                {
                    Address = model.Address,
                    City = model.City,
                    ZipCode = model.ZipCode
                });

                var user = await _userManager.FindByEmailAsync(model.Email);

                string picturePath = "~/Content/AdminPictures/defaultAvatar.jpg";
                if (file != null)
                {
                    //Upload image
                    picturePath = FileUploader.UploadImage(file, "AdminPictures");
                }

                var admin = new Admin
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Location = location,
                    HasValidatedEmail = false,
                    IdentityId = user.Id,
                    PicturePath = picturePath
                };

                adminService.AddAdmin(admin);

                TempData["Message"] = "Inscription effectuée. Vous allez recevoir un email pour valider votre compte.";
                //Send Verification Email
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                SendVerificationEmail(admin, user.Id, token, false);

                return RedirectToAction("Login");

            }
            else
            {
                TempData["ErrorMessage"] = result.Errors.ToString();
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ConfirmClientEmail(string userId, string code)
        {
            var client = clientService.GetClientByIdentityId(userId);
            if (!client.HasValidatedEmail)
            {
                client.HasValidatedEmail = true;
                clientService.UpdateClient(client);

                return View();
            }
            return View("NotFound");
        }

        [HttpGet]
        public IActionResult ConfirmAdminEmail(string userId, string code)
        {
            var admin = adminService.GetAdminByIdentityId(userId);
            if (!admin.HasValidatedEmail)
            {
                admin.HasValidatedEmail = true;
                adminService.EditAdmin(admin);

                TempData["Message"] = "Votre compte a été activé. Vous pouvez vous connecter maintenant.";
                return RedirectToAction("Login");
            }
            return View("NotFound");
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            var newToken = token.Replace(" ", "+");
            var model = new ResetPasswordViewModel { IdentityId = userId, Token = newToken };

            return View(model);
        }

        public async Task<IActionResult> ResetPassword([Bind("Password, RepeatedPassword")]ResetPasswordViewModel model, string userId, string token)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                return View("SuccessfulPasswordReset");
            }
            return View(model);
        }


        private async void SendVerificationEmail(Admin admin, string userId, string code, bool editedEmail)
        {
            var callBackUrl = "https://localhost:44352/Account/ConfirmAdminEmail?userId=" + userId + "&code=" + code;

            string parent = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            string path;
            if (editedEmail)
            {
                path = Path.Combine(parent, "DeliveryApp\\wwwroot\\Templates\\EmailTemplates\\EditedEmailAddress.html");
            }
            else
            {
                path = Path.Combine(parent, "DeliveryApp\\wwwroot\\Templates\\EmailTemplates\\ConfirmRegistration.html");
            }

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(path))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(
                builder.HtmlBody,
                admin.FirstName + " " + admin.LastName,
                callBackUrl
                );

            await emailSenderService.SendUserConfirmationEmail(
                admin.Email,
                admin.FirstName + " " + admin.LastName,
                messageBody
                );
        }
    }
}