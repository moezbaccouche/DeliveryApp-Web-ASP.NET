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
    [Route("delivery-app/cart")]
    public class CartController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ICartProductService cartProductService;
        private readonly IClientService clientService;

        public CartController(IProductService productService, ICartProductService cartProductService,
            IClientService clientService)
        {
            this.productService = productService;
            this.cartProductService = cartProductService;
            this.clientService = clientService;
        }

        [EnableCors("AllowAll")]
        [HttpPost("add")]
        public ActionResult<CartProduct> AddToCart([FromBody] ProductForCart productForCart)
        {
            if (productForCart == null)
            {
                return BadRequest();
            }

            Product product = productService.GetProductById(productForCart.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            Client client = clientService.GetClientById(productForCart.ClientId);
            if (client == null)
            {
                return NotFound();
            }


            var cartProduct = cartProductService.AddProduct(new CartProduct
            {
                ProductId = product.Id,
                Amount = productForCart.Amount,
                ClientId = client.Id
            });

            return Ok(cartProduct);
        }
    }
}