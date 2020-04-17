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

        public OrderController(IClientService clientService, IOrderService orderService,
            IProductOrderService productOrderService, IProductService productService,
            ICartProductService cartProductService)
        {
            this.clientService = clientService;
            this.orderService = orderService;
            this.productOrderService = productOrderService;
            this.productService = productService;
            this.cartProductService = cartProductService;
        }

        [EnableCors("AllowAll")]
        [HttpPost("add")]
        public ActionResult<Order> AddNewOrder(OrderForCreationDto orderDto)
        {
            var client = clientService.GetClientById(orderDto.ClientId);
            if(client == null)
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

            foreach(var prod in cartProducts)
            {
                //Add each product that was in the cart to the table ProductOrder
                var product = productService.GetProductById(prod.ProductId);
                if(product != null)
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
            if(client == null)
            {
                return NotFound();
            }

            var order = orderService.GetClientNotDeliveredOrder(clientId);
            if(order == null)
            {
                return Ok(new { nbOrders = 0 });
            }
            return Ok(new { order = order, nbOrders = 1 });
        }
    }
}
