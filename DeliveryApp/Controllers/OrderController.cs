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
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IDeliveryManService deliveryManService;
        private readonly IProductOrderService productOrderService;
        private readonly IProductImageService productImageService;
        private readonly IDeliveryInfoService deliveryInfoService;

        public OrderController(IOrderService orderService,
            IDeliveryManService deliveryManService,
            IProductOrderService productOrderService,
            IProductImageService productImageService, IDeliveryInfoService deliveryInfoService)
        {
            this.orderService = orderService;
            this.deliveryManService = deliveryManService;
            this.productOrderService = productOrderService;
            this.productImageService = productImageService;
            this.deliveryInfoService = deliveryInfoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotDeliveredOrders()
        {
            var notDeliveredOrders = orderService.GetNotDeliveredOrders();
            var availableDeliveryMen = deliveryManService.GetAllAvailableDeliveryMen();
            OrdersViewModel ordersViewModel = new OrdersViewModel
            {
                NotDeliveredOrders = notDeliveredOrders,
                AvailableDeliveryMen = availableDeliveryMen
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

        [HttpGet]
        public IActionResult DeliveryManLocation(int id)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(id);
            var model = new DeliveryMenViewModel { DeliveryMan = deliveryMan };

            return PartialView(model);
        }

        [HttpGet]
        public IActionResult OrderProducts(int id)
        {
            var order = orderService.GetOrderById(id);
            var orderProducts = productOrderService.GetOrderProducts(order);
            List<ProductImage> productsImages = new List<ProductImage>();
            foreach (var product in orderProducts)
            {
                var img = productImageService.GetProductImages(product.Article).FirstOrDefault();
                productsImages.Add(img);
            }


            var model = new OrderProductsViewModel
            {
                OrderProducts = orderProducts,
                ProductImages = productsImages
            };

            return PartialView("_OrderProducts", model);
        }

        [HttpGet]
        public void BindOrder(int idOrder, int deliveryManId)
        {
            var order = orderService.GetOrderById(idOrder);
            var deliveryMan = deliveryManService.GetDeliveryManById(deliveryManId);

            //Calculate estimated delivery time
            var estimatedDeliveryTime = DateTime.Now;

            //Create deliveryInfo entity
            var orderInfo = deliveryInfoService.AddDeliveryInfo(new DeliveryInfo
            {
                DeliveryMan = deliveryMan,
                EstimatedDeliveryTime = estimatedDeliveryTime,
                Order = order
            });

            //Edit the order status
            order.Status = EnumOrderStatus.InDelivery;
            orderService.EditOrder(order);

            //orderService.BindOrder(order, deliveryMan);

            deliveryMan.IsAvailable = false;
            deliveryManService.EditDeliveryMan(deliveryMan);

            TempData["Message"] = $"Commande affectée à { deliveryMan.FirstName } { deliveryMan.LastName } !";
        }


    }
}