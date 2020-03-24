using DeliveryApp.Data;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepo;
        private readonly ApplicationDbContext context;

        public ProductService(IRepository<Product> productRepo, ApplicationDbContext context)
        {
            this.productRepo = productRepo;
            this.context = context;
        }

        public Product AddProduct(Product newProduct)
        {
            productRepo.Insert(newProduct);
            return newProduct;
        }

        public Product DeleteProduct(int productId)
        {
            var product = productRepo.TableNoTracking.Where(p => p.Id == productId).FirstOrDefault();
            if(product != null)
            {
                productRepo.Delete(product);
            }
            return product;
        }

        public Product EditProduct(Product product)
        {
            productRepo.Update(product);
            return product;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var query = (from p in productRepo.TableNoTracking 
                         select p)
                         .ToList();
            return query;
        }

        public Product GetProductById(int productId)
        {
            var product = productRepo.TableNoTracking.Where(p => p.Id == productId).FirstOrDefault();
            return product;
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            var query = from p in productRepo.GetAll()
                        where p.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby p.Name
                        select p;
            return query;
        }
    }
}
