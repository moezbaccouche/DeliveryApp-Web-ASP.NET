using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace DeliveryApp.API.ControllersAPI
{
    [ApiController]
    [Route("delivery-app/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IClientService clientService;
        private readonly IOrderService orderService;
        private readonly IProductOrderService productOrderService;
        private readonly IProductService productService;
        private readonly ICartProductService cartProductService;
        private readonly IDeliveryInfoService deliveryInfoService;
        private readonly IDeliveryManService deliveryManService;
        private readonly IRatingService ratingService;

        public OrderController(IClientService clientService, IOrderService orderService,
            IProductOrderService productOrderService, IProductService productService,
            ICartProductService cartProductService, IDeliveryInfoService deliveryInfoService,
            IDeliveryManService deliveryManService, IRatingService ratingService)
        {
            this.clientService = clientService;
            this.orderService = orderService;
            this.productOrderService = productOrderService;
            this.productService = productService;
            this.cartProductService = cartProductService;
            this.deliveryInfoService = deliveryInfoService;
            this.deliveryManService = deliveryManService;
            this.ratingService = ratingService;
        }

        [EnableCors("AllowAll")]
        [HttpPost("add")]
        public ActionResult<Order> AddNewOrder(OrderForCreationDto orderDto)
        {
            var client = clientService.GetClientById(orderDto.ClientId);
            if (client == null)
            {
                return NotFound();
            }

            //Calculate delivery price
            var deliveryPrice = 5;

            var order = orderService.AddOrder(new Order
            {
                IdClient = client.Id,
                DeliveryPrice = deliveryPrice,
                OrderTime = DateTime.Now,
                Status = EnumOrderStatus.NotDelivered
            });

            var cartProducts = cartProductService.GetCartProducts(orderDto.ClientId);

            foreach (var prod in cartProducts)
            {
                //Add each product that was in the cart to the table ProductOrder
                var product = productService.GetProductById(prod.ProductId);
                if (product != null)
                {
                    var orderProduct = productOrderService.AddProduct(new ProductOrder
                    {
                        IdProduct = product.Id,
                        Amount = prod.Amount,
                        IdOrder = order.Id
                    });
                }
            }
            return Ok(order);
        }

        [EnableCors("AllowAll")]
        [HttpGet("notDelivered/{clientId}")]
        public ActionResult<Order> GetNotDeliveredOrders(int clientId)
        {
            var client = clientService.GetClientById(clientId);
            if (client == null)
            {
                return NotFound();
            }

            var order = orderService.GetClientNotDeliveredOrder(clientId);
            if (order == null)
            {
                return Ok(new { nbOrders = 0 });
            }
            return Ok(new { order = order, nbOrders = 1 });
        }

        [EnableCors("AllowAll")]
        [HttpGet("clients/{clientId}")]
        public ActionResult<IEnumerable<OrderInfosDto>> GetClientTreatedOrders(int clientId)
        {
            var client = clientService.GetClientById(clientId);
            if (client == null)
            {
                return NotFound();
            }

            var orders = orderService.GetClientTreatedOrders(clientId);
            var ordersInfos = new List<OrderInfosDto>();

            foreach (var order in orders)
            {
                var deliveryInfos = deliveryInfoService.GetOrderDeliveryInfo(order.Id);
                var orderDeliveryMan = deliveryManService.GetDeliveryManById(deliveryInfos.IdDeliveryMan);

                // We have to consider the situation where the deliveryMan object is null

                var orderInfo = new OrderInfosDto
                {
                    OrderId = order.Id,
                    OrderTime = order.OrderTime,
                    OrderPrice = order.OrderPrice,
                    OrderStatus = order.Status,
                    DeliveryPrice = order.DeliveryPrice,
                    DeliveryManId = orderDeliveryMan.Id,
                    DeliveryManName = $"{orderDeliveryMan.FirstName} {orderDeliveryMan.LastName}",
                    DeliveryManPicture = orderDeliveryMan.ImageBase64,
                    RealDeliveryTime = deliveryInfos.RealDeliveryTime
                };
                ordersInfos.Add(orderInfo);
            }

            return Ok(ordersInfos);
        }

        [EnableCors("AllowAll")]
        [HttpGet("{orderId}")]
        public ActionResult<OrderDetailsDto> GetOrderDetails(int orderId)
        {
            var order = orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var orderProducts = productOrderService.GetOrderProducts(order);
            var products = new List<ProductForCheckout>();
            foreach (var prod in orderProducts)
            {
                var product = productService.GetProductById(prod.IdProduct);

                products.Add(new ProductForCheckout 
                { 
                    Id = product.Id,
                    Amount = prod.Amount,
                    ImageBase64 = product.ProductImages.FirstOrDefault().ImageBase64,
                    Name = product.Name 
                });
            }

            var deliveryInfos = deliveryInfoService.GetOrderDeliveryInfo(order.Id);
            var orderDeliveryMan = deliveryManService.GetDeliveryManById(deliveryInfos.IdDeliveryMan);

            var deliveryManClientRating = ratingService.GetClientRatingForDeliveryMan(order.IdClient, orderDeliveryMan.Id);
            var rating = 0;
            if(deliveryManClientRating != null)
            {
                rating = deliveryManClientRating.Rate;
            }

            var orderDetails = new OrderDetailsDto
            {
                OrderId = order.Id,
                OrderTime = order.OrderTime,
                OrderPrice = order.OrderPrice,
                OrderStatus = order.Status,
                Products = products,
                DeliveryPrice = order.DeliveryPrice,
                DeliveryManId = orderDeliveryMan.Id,
                DeliveryManName = $"{orderDeliveryMan.FirstName} {orderDeliveryMan.LastName}",
                DeliveryManPicture = orderDeliveryMan.ImageBase64,
                DeliveryManClientRating = rating,
                RealDeliveryTime = deliveryInfos.RealDeliveryTime
            };

            return Ok(orderDetails);
        }

    }
}
