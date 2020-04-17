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
    public class ProductOrderService : IProductOrderService
    {
        private readonly IRepository<ProductOrder> repoProductOrder;

        public ProductOrderService(IRepository<ProductOrder> repoProductOrder)
        {
            this.repoProductOrder = repoProductOrder;
        }

        public IEnumerable<ProductOrder> GetOrderProducts(Order order)
        {
            var productOrders = repoProductOrder.TableNoTracking
                .Where(po => po.Order.Id == order.Id)
                .Include(po => po.Order)
                .Include(po => po.Article)
                .ToList();

            return productOrders;
        }

        public IEnumerable<ProductOrder> GetAllOrderProducts()
        {
            var allProductOrders = repoProductOrder.TableNoTracking
                   .Include(po => po.Order)
                   .Include(po => po.Article)
                   .ToList();

            return allProductOrders;
        }

        public ProductOrder AddProduct(ProductOrder newProduct)
        {
            if(newProduct != null)
            {
                repoProductOrder.Insert(newProduct);
            }
            return newProduct;
        }
    }
}
