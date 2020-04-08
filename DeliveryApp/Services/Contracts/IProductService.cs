using DeliveryApp.Models.Data;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface IProductService
    {
        Product AddProduct(Product newProduct);
        Product EditProduct(Product product);
        Product DeleteProduct(int productId);
        Product GetProductById(int productId);
        IEnumerable<Product> GetProductsByName(string name);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetAllProducts(string order);
        IEnumerable<Product> GetProductsByCategory(Category category);
    }
}
