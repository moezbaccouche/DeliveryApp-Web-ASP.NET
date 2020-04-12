using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Extensions;
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
        private readonly ICategoryService categoryService;

        public CartController(IProductService productService, ICartProductService cartProductService,
            IClientService clientService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.cartProductService = cartProductService;
            this.clientService = clientService;
            this.categoryService = categoryService;
        }

        [EnableCors("AllowAll")]
        [HttpGet("products/clients/{clientId}")]
        public ActionResult<IEnumerable<ProductForCheckout>> GetCartProducts(int clientId)
        {
            var client = clientService.GetClientById(clientId);
            if (client == null)
            {
                return NotFound();
            }
            var cartProducts = cartProductService.GetCartProducts(client.Id);

            var categories = new List<Category>();
            List<ProductForCheckout> allProducts = new List<ProductForCheckout>();
            foreach(var prod in cartProducts)
            {
                var product = productService.GetProductById(prod.ProductId);
                var productCategory = categoryService.GetCategoryById(product.CategoryId);

                if(!categories.CategoryExists(productCategory))
                {
                    categories.Add(productCategory);
                }

                allProducts.Add(new ProductForCheckout
                {
                    Id = product.Id,
                    Name = product.Name,
                    ImageBase64 = product.ProductImages.FirstOrDefault().ImageBase64,
                    Amount = prod.Amount,
                    //The following code line wont work if the amount can't be converted to int
                    TotalProductPrice = product.Price * Convert.ToInt32(prod.Amount),
                    Category = productCategory.Name
                });
            }

            CartDto cartDto = new CartDto { Categories = categories, Products = allProducts };
            return Ok(cartDto);
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

            var cartProduct = cartProductService.GetProduct(productForCart.ClientId, productForCart.ProductId);

            /*
             * If cartProduct is null this means that the cart doesn't have the selected product
             * then we will add it to the DB
             */
            if (cartProduct == null)
            {
                cartProduct = cartProductService.AddProduct(new CartProduct
                {
                    ProductId = product.Id,
                    Amount = productForCart.Amount,
                    ClientId = client.Id
                });
            }
            else
            {
                /*
                 * If cartProduct is not null, it means that the user has already this product in his cart
                 * then we will increment the amount of the desired product and update the entity
                 */

                if (Int32.TryParse(cartProduct.Amount, out int productAmount))
                {
                    productAmount += Convert.ToInt32(productForCart.Amount);
                    cartProduct.Amount = productAmount.ToString();
                }
                cartProductService.EditProduct(cartProduct);
            }
            return Ok(cartProduct);
        }
    }
}