using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface IDeliveryManService
    {
        DeliveryMan AddDeliveryMan(DeliveryMan newDeliveryMan);
        DeliveryMan ValidateDeliveryMan(DeliveryMan deliveryMan);
        DeliveryMan DeleteDeliveryMan(int id);
        DeliveryMan GetDeliveryManById(int id);
        IEnumerable<DeliveryMan> GetNotValidatedDeliveryMen();
        IEnumerable<DeliveryMan> GetAllDeliveryMen();
        IEnumerable<DeliveryMan> GetAllAvailableDeliveryMen();
    }
}
