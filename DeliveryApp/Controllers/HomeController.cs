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
using DeliveryApp.Models.DTO;

namespace DeliveryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService orderService;
        private readonly IClientService clientService;
        private readonly IDeliveryManService deliveryManService;
        private readonly IRatingService ratingService;
        private readonly IDeliveryInfoService deliveryInfoService;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService, IClientService clientService,
            IDeliveryManService deliveryManService, IRatingService ratingService,
            IDeliveryInfoService deliveryInfoService)
        {
            _logger = logger;
            this.orderService = orderService;
            this.clientService = clientService;
            this.deliveryManService = deliveryManService;
            this.ratingService = ratingService;
            this.deliveryInfoService = deliveryInfoService;
        }

        public IActionResult Index()
        {
            var nbPendingOrders = orderService.GetAllPendingOrders().Count();
            var deliveredOrders = orderService.GetAllDeliveredOrders();
            var nbClients = clientService.GetAllClients().Count();
            var nbNewDeliveryMen = deliveryManService.GetNotValidatedDeliveryMen().Count();

            var allDeliveryMen = deliveryManService.GetAllDeliveryMen();
            var ratedDeliveryMen = new List<RatedDeliveryManDto>();
            var deliveredOrdersPerDeliveryMan = new List<DeliveredOrdersPerDeliveryManDto>();
            foreach (var man in allDeliveryMen)
            {
                var ratingOverall = ratingService.GetDeliveryManRatingOverall(man.Id);
                if(ratingOverall != 0)
                {
                    ratedDeliveryMen.Add(new RatedDeliveryManDto
                    {
                        FirstName = man.FirstName,
                        LastName = man.LastName,
                        RatingOverall = ratingOverall
                    });
                }

                var infos = deliveryInfoService.GetDeliveryManOrderHistory(man.Id);
                if(infos.Count() != 0)
                {
                    var nbDeliveredOrders = 0;
                    foreach(var info in infos)
                    {
                        var order = orderService.GetOrderById(info.IdOrder);
                        if(order.Status == Models.Data.EnumOrderStatus.Delivered)
                        {
                            nbDeliveredOrders++;
                        }
                    }
                    var deliveryMan = new DeliveredOrdersPerDeliveryManDto
                    {
                        FirstName = man.FirstName,
                        LastName = man.LastName,
                        NbDeliveredOrders = nbDeliveredOrders
                    };
                    deliveredOrdersPerDeliveryMan.Add(deliveryMan);
                }
            }


            var dashboardViewModel = new DashboardViewModel
            {
                NbPendingOrders = nbPendingOrders,
                NbClients = nbClients,
                DeliveredOrders = deliveredOrders,
                NbNewDeliveryMen = nbNewDeliveryMen,
                RatedDeliveryMen = ratedDeliveryMen,
                DeliveredOrdersPerDeliveryMan = deliveredOrdersPerDeliveryMan
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
