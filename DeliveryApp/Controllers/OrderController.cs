using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Models.ViewModels;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotDeliveredOrders()
        {
            var notDeliveredOrders = orderService.GetNotDeliveredOrders();
            OrdersViewModel ordersViewModel = new OrdersViewModel
            {
                NotDeliveredOrders = notDeliveredOrders
            };
            return View(ordersViewModel);
        }

        public IActionResult DeliveredOrders()
        {
            var deliveredOrders = orderService.GetDeliveredOrders();
            OrdersViewModel ordersViewModel = new OrdersViewModel
            {
                DeliveredOrders = deliveredOrders
            };
            return View(ordersViewModel);
        }

        public IActionResult InDeliveryOrders()
        {
            var inDeliveryOrders = orderService.GetInDeliveryOrders();
            OrdersViewModel ordersViewModel = new OrdersViewModel
            {
                InDeliveryOrders = inDeliveryOrders
            };
            return View(ordersViewModel);
        }
    }
}