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
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> repoOrder;

        public OrderService(IRepository<Order> repoOrder)
        {
            this.repoOrder = repoOrder;
        }

        public Order AddOrder(Order order)
        {
            repoOrder.Insert(order);
            return order;
        }


        public Order DeleteOrder(int id)
        {
            var order = GetOrderById(id);
            if (order != null)
            {
                repoOrder.Delete(order);
            }
            return order;
        }

        public Order EditOrder(Order order)
        {
            repoOrder.Update(order);
            return order;
        }

        public Order GetClientPendingOrder(int clientId)
        {
            var notDeliveredOrder = repoOrder.TableNoTracking
                .Where(o => o.Status == EnumOrderStatus.Pending && o.IdClient == clientId)
                .FirstOrDefault();

            return notDeliveredOrder;
        }

        public IEnumerable<Order> GetClientTreatedOrders(int clientId)
        {
            var treatedOrders = repoOrder.TableNoTracking
                .Where(o => o.IdClient == clientId && o.Status != EnumOrderStatus.Pending)
                .ToList();

            return treatedOrders;
        }

        public IEnumerable<Order> GetDeliveredOrders()
        {
            var deliveredOrders = repoOrder.TableNoTracking
                .Where(o => o.Status == EnumOrderStatus.Delivered)
                .Include(o => o.DeliveryMan)
                .Include(o => o.Client)
                .ToList();

            return deliveredOrders;
        }

        public IEnumerable<Order> GetInDeliveryOrders()
        {
            var deliveredOrders = repoOrder.TableNoTracking
                .Where(o => o.Status == EnumOrderStatus.InDelivery)
                .Include(o => o.DeliveryMan)
                .Include(o => o.DeliveryMan.Location)
                .Include(o => o.Client)
                .ToList();

            return deliveredOrders;
        }

        public IEnumerable<Order> GetPendingOrders()
        {
            var deliveredOrders = repoOrder.TableNoTracking
                .Where(o => o.Status == EnumOrderStatus.Pending)
                .Include(o => o.DeliveryMan)
                .Include(o => o.Client)
                .ToList();

            return deliveredOrders;
        }

        public Order GetOrderById(int id)
        {
            var order = repoOrder.TableNoTracking.Where(o => o.Id == id).FirstOrDefault();
            return order;
        }

        public int GetClientNbDeliveredProducts(int clientId)
        {
            return repoOrder.TableNoTracking
                .Where(o => o.IdClient == clientId && o.Status == EnumOrderStatus.Delivered)
                .Count();
        }

        public IEnumerable<Order> GetClientOrders(int clientId)
        {
            var orders = repoOrder.TableNoTracking
                .Where(o => o.IdClient == clientId)
                .ToList();

            return orders;
        }
    }
}
