using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface IProductImageService
    {
        ProductImage AddProductImage(ProductImage newProductImage);
        IEnumerable<ProductImage> GetProductImages(Product product);
        ProductImage GetProductImageById(int id);
        ProductImage DeleteProductImage(ProductImage image);
    }
}
