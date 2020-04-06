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
        private readonly IProductImageService productImageService;

        public ProductService(IRepository<Product> productRepo, ApplicationDbContext context,
            IProductImageService productImageService)
        {
            this.productRepo = productRepo;
            this.context = context;
            this.productImageService = productImageService;
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
            var products = (from p in productRepo.TableNoTracking 
                            orderby p.Name 
                            select p)
                         .ToList();

            foreach (var prod in products)
            {
                foreach (var img in productImageService.GetProductImages(prod))
                {
                    prod.ProductImages.Add(img);
                }

            }

            return products;
        }

        public Product GetProductById(int productId)
        {
            var product = productRepo.TableNoTracking.Where(p => p.Id == productId).FirstOrDefault();
            if(product != null)
            {
                product.ProductImages = productImageService.GetProductImages(product) as List<ProductImage>;
            }

            return product;
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            var products = (from p in productRepo.GetAll()
                        where p.Name.ToLower().Contains(name)
                        orderby p.Name
                        select p)
                        .ToList();


            foreach (var prod in products)
            {
                foreach (var img in productImageService.GetProductImages(prod))
                {
                    prod.ProductImages.Add(img);
                }

            }

            return products;
        }

        public IEnumerable<Product> GetAllProducts(string searchQuery)
        {
            IEnumerable<Product> products = null;

            string searchTerm = null;
            if(searchQuery != null)
            {
                searchTerm = searchQuery.Trim().ToLower();
            }

            if(string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAllProducts();
            }

            if(searchTerm == "desc")
            {
                products = (from p in productRepo.TableNoTracking
                           orderby p.Price descending
                           select p)
                           .ToList();
            }
            else
            {
                if(searchTerm == "asc")
                {
                    products = (from p in productRepo.TableNoTracking
                                orderby p.Price ascending
                                select p)
                           .ToList();
                }
                else
                {
                    return GetProductsByName(searchTerm);
                }
            }

            foreach(var prod in products)
            {
                foreach(var img in productImageService.GetProductImages(prod))
                {
                    prod.ProductImages.Add(img);
                }
                
            }

            return products;
        }

    }
}
