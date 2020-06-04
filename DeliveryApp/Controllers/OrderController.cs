using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Extensions;
using DeliveryApp.Models.Data;
using DeliveryApp.Models.DTO;
using DeliveryApp.Models.ViewModels;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace DeliveryApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IDeliveryManService deliveryManService;
        private readonly IProductOrderService productOrderService;
        private readonly IProductImageService productImageService;
        private readonly IDeliveryInfoService deliveryInfoService;
        private readonly IClientService clientService;
        private readonly IEmailSenderService emailSenderService;
        private readonly IProductService productService;
        private readonly ICurrentLocationService currentLocationService;

        public OrderController(IOrderService orderService,
            IDeliveryManService deliveryManService,
            IProductOrderService productOrderService,
            IProductImageService productImageService, IDeliveryInfoService deliveryInfoService,
            IClientService clientService, IEmailSenderService emailSenderService, IProductService productService,
            ICurrentLocationService currentLocationService)
        {
            this.orderService = orderService;
            this.deliveryManService = deliveryManService;
            this.productOrderService = productOrderService;
            this.productImageService = productImageService;
            this.deliveryInfoService = deliveryInfoService;
            this.clientService = clientService;
            this.emailSenderService = emailSenderService;
            this.productService = productService;
            this.currentLocationService = currentLocationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PendingOrders()
        {
            var pendingOrders = orderService.GetAllPendingOrders();
            var pendingOrdersDto = new List<PendingOrderDto>();
            foreach (var order in pendingOrders)
            {
                var client = clientService.GetClientById(order.IdClient);
                pendingOrdersDto.Add(new PendingOrderDto { PendingOrder = order, Client = client });
            }

            var deliveryMen = deliveryManService.GetAllDeliveryMen();

            var pendingOrdersViewModel = new PendingOrdersViewModel
            {
                PendingOrders = pendingOrdersDto,
                AllDeliveryMen = deliveryMen
            };

            return View(pendingOrdersViewModel);
        }

        [HttpGet]
        public IActionResult ProcessingOrders()
        {
            var processingOrders = orderService.GetAllProcessingOrders();

            var processingOrdersDto = new List<ProcessingOrderDto>();
            foreach (var order in processingOrders)
            {
                var client = clientService.GetClientById(order.IdClient);
                var info = deliveryInfoService.GetOrderDeliveryInfo(order.Id);
                var deliveryMan = deliveryManService.GetDeliveryManById(info.IdDeliveryMan);

                processingOrdersDto.Add(new ProcessingOrderDto
                {
                    ProcessingOrder = order,
                    Client = client,
                    DeliveryMan = deliveryMan,
                    DeliveryInfo = info
                });
            }

            var processingOrdersViewModel = new ProcessingOrderViewModel
            {
                ProcessingOrders = processingOrdersDto
            };

            return View(processingOrdersViewModel);
        }

        [HttpGet]
        public IActionResult InDeliveryOrders()
        {
            var inDeliveryOrders = orderService.GetAllInDeliveryOrders();

            var inDeliveryOrdersDto = new List<InDeliveryOrderDto>();
            foreach (var order in inDeliveryOrders)
            {
                var client = clientService.GetClientById(order.IdClient);
                var info = deliveryInfoService.GetOrderDeliveryInfo(order.Id);
                var deliveryMan = deliveryManService.GetDeliveryManById(info.IdDeliveryMan);
                var deliveryManCurrentLocation = currentLocationService.GetDeliveryManCurrentLocation(deliveryMan.Id);

                inDeliveryOrdersDto.Add(new InDeliveryOrderDto
                {
                    InDeliveryOrder = order,
                    Client = client,
                    DeliveryMan = deliveryMan,
                    DeliveryInfo = info,
                    DeliveryManCurrentLocation = deliveryManCurrentLocation
                });
            }

            var inDeliveryOrdersViewModel = new InDeliveryOrdersViewModel
            {
                InDeliveryOrders = inDeliveryOrdersDto
            };

            return View(inDeliveryOrdersViewModel);
        }

        [HttpGet]
        public IActionResult DeliveredOrders()
        {
            var deliveredOrders = orderService.GetAllDeliveredOrders();

            var deliveredOrdersDto = new List<DeliveredOrderDto>();
            foreach (var order in deliveredOrders)
            {
                var client = clientService.GetClientById(order.IdClient);
                var info = deliveryInfoService.GetOrderDeliveryInfo(order.Id);
                var deliveryMan = deliveryManService.GetDeliveryManById(info.IdDeliveryMan);

                deliveredOrdersDto.Add(new DeliveredOrderDto
                {
                    DeliveredOrder = order,
                    Client = client,
                    DeliveryMan = deliveryMan,
                    DeliveryInfo = info
                });
            }

            var deliveredOrdersViewModel = new DeliveredOrdersViewModel
            {
                DeliveredOrders = deliveredOrdersDto
            };

            return View(deliveredOrdersViewModel);
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

            var dto = new List<OrderProductDto>();
            foreach(var prod in orderProducts)
            {
                var product = productService.GetProductById(prod.IdProduct);
                dto.Add(new OrderProductDto
                {
                    Product = product,
                    ProductImage = productImageService.GetProductImages(product).FirstOrDefault(),
                    OrderProduct = prod
                });
            }

            var model = new OrderProductsViewModel
            {
                OrderProducts = dto,
            };

            return PartialView("_OrderProducts", model);
        }

        [HttpGet]
        public void BindOrder(int idOrder, int deliveryManId)
        {
            var order = orderService.GetOrderById(idOrder);
            var deliveryMan = deliveryManService.GetDeliveryManById(deliveryManId);
            var client = clientService.GetClientById(order.IdClient);

            //Calculate estimated delivery time
            var estimatedDeliveryTime = DateTime.Now.AddMinutes(30);

            //Create deliveryInfo entity
            deliveryInfoService.AddDeliveryInfo(new DeliveryInfo
            {
                IdDeliveryMan = deliveryManId,
                EstimatedDeliveryTime = estimatedDeliveryTime,
                IdOrder = idOrder,
                AcceptingOrderTime = DateTime.Now
            });

            //Edit the order status
            order.Status = EnumOrderStatus.Processing;
            orderService.EditOrder(order);


            SendBoundOrderDeliveryEmail(deliveryMan);

            //Send push notification
            //To DeliveryMan
            PushNotificationsSender.SendPushNotificationToSpecificUsers(new String[] { deliveryMan.PlayerId }, "Livraison affecté", "L'administrateur vous a affectée à une livraison.");
           
            //To Client
            PushNotificationsSender.SendPushNotificationToSpecificUsers(new String[] { client.PlayerId }, "Commande acceptée", "Votre commande a été acceptée. Elle est maintenant en cours de traitement");

            TempData["Message"] = $"Commande affectée à { deliveryMan.FirstName } { deliveryMan.LastName } !";
        }


        private async void SendBoundOrderDeliveryEmail(DeliveryMan deliveryMan)
        {
            string parent = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            string path;

            path = Path.Combine(parent, "DeliveryApp\\wwwroot\\Templates\\EmailTemplates\\BoundOrderDeliveryEmail.html");


            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(path))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(
                builder.HtmlBody,
                deliveryMan.FirstName + " " + deliveryMan.LastName
                );

            await emailSenderService.SendBoundOrderDeliveryEmail(
                deliveryMan.Email,
                deliveryMan.FirstName + " " + deliveryMan.LastName,
                messageBody
                );
        }


    }
}