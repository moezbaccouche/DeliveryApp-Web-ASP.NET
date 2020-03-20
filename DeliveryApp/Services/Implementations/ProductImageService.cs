using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{
    public class ProductImageService : IProductImageService
    {
        private readonly IRepository<ProductImage> repoProductImage;

        public ProductImageService(IRepository<ProductImage> repoProductImage)
        {
            this.repoProductImage = repoProductImage;
        }

        public ProductImage AddProductImage(ProductImage newProductImage)
        {
            repoProductImage.Insert(newProductImage);
            return newProductImage;
        }
    }
}
