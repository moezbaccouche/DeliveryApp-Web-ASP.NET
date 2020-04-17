using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CartController(IProductService productService, ICartProductService cartProductService,
            IClientService clientService, ICategoryService categoryService, IMapper mapper)
        {
            this.productService = productService;
            this.cartProductService = cartProductService;
            this.clientService = clientService;
            this.categoryService = categoryService;
            _mapper = mapper;
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

            var categories = new List<CategoryForCartDto>();
            List<ProductForCheckout> allProducts = new List<ProductForCheckout>();
            foreach (var prod in cartProducts)
            {
                var product = productService.GetProductById(prod.ProductId);
                var productCategory = categoryService.GetCategoryById(product.CategoryId);

                if (!categories.CategoryExists(productCategory))
                {
                    categories.Add(new CategoryForCartDto {Id = productCategory.Id, Name = productCategory.Name, NbProducts = 1 });
                }
                else
                {
                    categories.Where(c => c.Id == productCategory.Id).FirstOrDefault().NbProducts++;
                }

                allProducts.Add(new ProductForCheckout
                {
                    Id = product.Id,
                    Name = product.Name,
                    ImageBase64 = product.ProductImages.FirstOrDefault().ImageBase64,
                    Amount = prod.Amount,
                    //The following code line wont work if the amount can't be converted to int
                    TotalProductPrice = Math.Floor(1000 * (product.Price * Convert.ToInt32(prod.Amount))) / 1000,
                    Category = productCategory.Name,
                    CategoryId = productCategory.Id
                });
            }
            ClientForCartDto clientForCart = _mapper.Map<ClientForCartDto>(client);
            CartDto cartDto = new CartDto
            {
                Categories = categories,
                Products = allProducts,
                Client = clientForCart
            };
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


        [EnableCors("AllowAll")]
        [HttpPost("delete")]
        public ActionResult<CartProduct> DeleteProductFromCart([FromBody] ProductToDeleteFromCartDto productToDelete)
        {
            var client = clientService.GetClientById(productToDelete.ClientId);
            if (client == null)
            {
                return NotFound();
            }

            var product = productService.GetProductById(productToDelete.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var deletedProduct = cartProductService.RemoveProduct(client.Id, product.Id);
            return Ok(deletedProduct);
        }

        [EnableCors("AllowAll")]
        [HttpPost("deleteAll")]
        public ActionResult<IEnumerable<CartProduct>> DeleteAllProductsFromCart([FromBody] CartClientDto cartClient)
        {
            var client = clientService.GetClientById(cartClient.ClientId);
            if (client == null)
            {
                return NotFound();
            }

            var deletedProducts = cartProductService.RemoveAllProducts(client.Id);
            return Ok(deletedProducts);
        }

        [EnableCors("AllowAll")]
        [HttpPost("edit")]
        public ActionResult<CartProduct> EditProduct([FromBody] ProductForCart productForCart)
        {
            var client = clientService.GetClientById(productForCart.ClientId);
            if (client == null)
            {
                return NotFound();
            }

            var product = productService.GetProductById(productForCart.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var cartProduct = cartProductService.GetProduct(client.Id, product.Id);
            if (cartProduct != null)
            {
                cartProduct.Amount = productForCart.Amount;
            }

            var productToReturn = cartProductService.EditProduct(cartProduct);

            return Ok(productToReturn);
        }
    }
}