using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeliveryApp.Data;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using DeliveryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using DeliveryApp.Extensions;

namespace DeliveryApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAdminService adminService;
        private readonly IDeliveryManService deliveryManService;
        private readonly IClientService clientService;
        private readonly IOrderService orderService;
        private readonly ILocationService locationService;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context, IAdminService adminService,
            IDeliveryManService deliveryManService, IClientService clientService, IOrderService orderService,
            ILocationService locationService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.adminService = adminService;
            this.deliveryManService = deliveryManService;
            this.clientService = clientService;
            this.orderService = orderService;
            this.locationService = locationService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var admin = adminService.GetAdminByEmail("ademo@gmail.com");

            var deliveryMen = deliveryManService.GetAllDeliveryMen();
            var allClients = clientService.GetAllClients();
            var allDeliveredOrders = orderService.GetAllDeliveredOrders();

            ProfileViewModel avm = new ProfileViewModel
            {
                Admin = admin,
                NbClients = allClients.Count(), 
                NbDeliveredOrders = allDeliveredOrders.Count(),
                DeliveryMen = deliveryMen
            };
            ViewData["IsActive"] = "true";
            return View(avm);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileViewModel pvm)
        {
            var admin = adminService.GetAdminById(pvm.Admin.Id);
            var adminLocation = locationService.GetLocationById(admin.Location.Id);
            admin.FirstName = pvm.Admin.FirstName;
            admin.LastName = pvm.Admin.LastName;
            admin.Phone = pvm.Admin.Phone;

            adminLocation.Address = pvm.Admin.Location.Address;
            adminLocation.City = pvm.Admin.Location.City;
            adminLocation.ZipCode = pvm.Admin.Location.ZipCode;

            if(admin.Email != pvm.Admin.Email)
            {
                //Email has been updated
                var user = await _userManager.FindByIdAsync(admin.IdentityId);
                if (user != null)
                {
                    if(await _userManager.FindByEmailAsync(pvm.Admin.Email) != null)
                    {
                        TempData["ErrorMessage"] = "Adresse Email déjà utilisée !";
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        user.Email = pvm.Admin.Email;
                        user.NormalizedEmail = pvm.Admin.Email.ToUpper();
                        user.UserName = pvm.Admin.Email;
                        user.NormalizedUserName = pvm.Admin.Email.ToUpper();
                        admin.Email = pvm.Admin.Email;
                        admin.HasValidatedEmail = true;

                        //Change the session email
                        await _userManager.UpdateAsync(user);
                    }
                }
            }

            admin.Location = adminLocation;
            locationService.UpdateLocation(adminLocation);
            adminService.EditAdmin(admin);


            TempData["Message"] = "Profil modifié avec succès !";
           

            return RedirectToAction("Profile");
        }

    }
}
