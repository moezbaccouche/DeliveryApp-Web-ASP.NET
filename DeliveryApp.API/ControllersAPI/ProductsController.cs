using AutoMapper;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.ControllersAPI
{
    [ApiController]
    [Route("delivery-app/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper _mapper;
        private readonly IClientService clientService;
        private readonly IFavoritesService favoritesService;

        public ProductsController(IProductService productService, IMapper mapper,
            IClientService clientService, IFavoritesService favoritesService)
        {
            this.productService = productService;
            _mapper = mapper;
            this.clientService = clientService;
            this.favoritesService = favoritesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductForHomeDto>> GetProducts(string orderQuery)
        {
            var products = productService.GetAllProducts(orderQuery);

            return Ok(_mapper.Map<IEnumerable<ProductForHomeDto>>(products));
        }

        [HttpGet("{productId}", Name = "GetProduct")]
        public ActionResult<ProductDetailsDto> GetProduct(int productId)
        {
            var product = productService.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            ICollection<byte[]> imagesBase64 = new List<byte[]>();
            foreach(var img in product.ProductImages)
            {
                imagesBase64.Add(img.ImageBase64);
            }

            //Could be optimized with IMapper
            ProductDetailsDto productDetails = new ProductDetailsDto 
            { 
                Description = product.Description,
                Id = product.Id, Name = product.Name,
                Price = product.Price,
                ProductImagesBase64 = imagesBase64
            };

            
            return Ok(productDetails);
        }

        [HttpPost("like-product")]
        public ActionResult<ProductForHomeDto> LikeProduct(FavoriteForCreationDto favoriteDto)
        {
            var client = clientService.GetClientById(favoriteDto.ClientId);
            var product = productService.GetProductById(favoriteDto.ProductId);

            if (client == null || product == null)
            {
                return NotFound();
            }

            var favorite = favoritesService.AddProductToFavorites(new Favorites { ProductId = favoriteDto.ProductId, ClientId = favoriteDto.ClientId });

            return CreatedAtRoute("GetProduct", new { productId = product.Id }, favorite);
        }

        [HttpPost("dislike-product")]
        public ActionResult<ProductForHomeDto> DislikeProduct(FavoriteForCreationDto favoriteDto)
        {
            var client = clientService.GetClientById(favoriteDto.ClientId);
            var product = productService.GetProductById(favoriteDto.ProductId);
            if (client == null || product == null)
            {
                return NotFound();
            }

            var favorite = favoritesService.RemoveProductFromFavorites(new Favorites { ProductId = favoriteDto.ProductId, ClientId = favoriteDto.ClientId });
            if(favorite == null)
            {
                return NotFound();
            }
            
            return Ok(favorite);
        }
    }
}
