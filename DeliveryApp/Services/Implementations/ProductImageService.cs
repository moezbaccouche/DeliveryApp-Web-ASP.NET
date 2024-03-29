﻿using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<ProductImage> GetProductImages(Product product)
        {
            var images = repoProductImage.TableNoTracking
                .Where(pi => pi.Product == product)
                .Include(pi => pi.Product)
                .ToList();
            return images;
        }

        public ProductImage GetProductImageById(int id)
        {
            var image = repoProductImage.TableNoTracking.Where(pi => pi.Id == id).FirstOrDefault();
            return image;
        }

        public ProductImage DeleteProductImage(ProductImage image)
        {
            repoProductImage.Delete(image);
            return image;
        }
    }
}
