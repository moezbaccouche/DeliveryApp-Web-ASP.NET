using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface IProductOrderService
    {
        IEnumerable<ProductOrder> GetOrderProducts(Order order);
        IEnumerable<ProductOrder> GetAllOrderProducts();
    }
}
