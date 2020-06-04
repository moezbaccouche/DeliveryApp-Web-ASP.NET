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
                .Where(po => po.IdOrder == order.Id)
                .ToList();

            return productOrders;
        }

        public IEnumerable<ProductOrder> GetAllOrderProducts()
        {
            var allProductOrders = repoProductOrder.TableNoTracking.ToList();

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

        public ProductOrder DeleteProduct(int orderId, int productId)
        {
            var product = repoProductOrder.TableNoTracking
                .Where(p => p.IdOrder == orderId && p.IdProduct == productId)
                .FirstOrDefault();

            repoProductOrder.Delete(product);

            return product;
        }

        public ProductOrder GetOrderProduct(int orderId, int productId)
        {
            var product = repoProductOrder.TableNoTracking
               .Where(p => p.IdOrder == orderId && p.IdProduct == productId)
               .FirstOrDefault();

            return product;
        }

        public ProductOrder EditOrderProduct(ProductOrder product)
        {
            repoProductOrder.Update(product);
            return product;
        }
    }
}
