using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{
    public class DeliveryInfoService : IDeliveryInfoService
    {
        private readonly IRepository<DeliveryInfo> repoDeliveryInfo;

        public DeliveryInfoService(IRepository<DeliveryInfo> repoDeliveryInfo)
        {
            this.repoDeliveryInfo = repoDeliveryInfo;
        }

        public DeliveryInfo AddDeliveryInfo(DeliveryInfo newDeliveryInfo)
        {
            if(newDeliveryInfo != null)
            {
                repoDeliveryInfo.Insert(newDeliveryInfo);
            }
            return newDeliveryInfo;
        }

        public DeliveryInfo EditDeliveryInfo(DeliveryInfo newDeliveryInfo)
        {
            if (newDeliveryInfo != null)
            {
                repoDeliveryInfo.Update(newDeliveryInfo);
            }
            return newDeliveryInfo;
        }

        public IEnumerable<DeliveryInfo> GetAllDeliveryInfos()
        {
            var infos = (from i in repoDeliveryInfo.TableNoTracking
                         select i)
                         .ToList();

            return infos;
        }

        public IEnumerable<DeliveryInfo> GetDeliveryManOrderHistory(int deliveryManId)
        {
            var ordersInfos = repoDeliveryInfo.TableNoTracking
                .Where(i => i.IdDeliveryMan == deliveryManId)
                .ToList();

            return ordersInfos;
        }

        public DeliveryInfo GetOrderDeliveryInfo(int orderId)
        {
            var info = (from i in repoDeliveryInfo.TableNoTracking
                        where i.IdOrder == orderId
                        select i)
                        .FirstOrDefault();
            return info;
        }

        public DeliveryInfo RemoveDeliveryInfo(DeliveryInfo deliveryInfo)
        {
            var info = (from i in repoDeliveryInfo.TableNoTracking
                        where i.Order.Id == deliveryInfo.Id
                        select i)
                        .FirstOrDefault();

            repoDeliveryInfo.Delete(info);
            return info;
        }
    }
}
