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

namespace DeliveryApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAdminService adminService;

        public AdminController(ApplicationDbContext context, IAdminService adminService)
        {
            _context = context;
            this.adminService = adminService;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var admin = adminService.GetAdminByEmail("moez_bac@hotmail.com");
            ProfileViewModel avm = new ProfileViewModel { Admin = admin };
            ViewData["IsActive"] = "true";
            return View(avm);
        }
    }
}
