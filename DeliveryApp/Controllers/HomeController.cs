using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeliveryApp.Models;
using DeliveryApp.Services.Contracts;
using DeliveryApp.Models.ViewModels;

namespace DeliveryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService orderService;
        private readonly IClientService clientService;
        private readonly IDeliveryManService deliveryManService;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService, IClientService clientService,
            IDeliveryManService deliveryManService)
        {
            _logger = logger;
            this.orderService = orderService;
            this.clientService = clientService;
            this.deliveryManService = deliveryManService;
        }

        public IActionResult Index()
        {
            var nbPendingOrders = orderService.GetAllPendingOrders().Count();
            var nbDeliveredOrders = orderService.GetAllDeliveredOrders().Count();
            var nbClients = clientService.GetAllClients().Count();
            var nbNewDeliveryMen = deliveryManService.GetNotValidatedDeliveryMen().Count();

            var dashboardViewModel = new DashboardViewModel
            {
                NbPendingOrders = nbPendingOrders,
                NbClients = nbClients,
                NbDeliveredOrders = nbDeliveredOrders,
                NbNewDeliveryMen = nbNewDeliveryMen
            };

            return View(dashboardViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
