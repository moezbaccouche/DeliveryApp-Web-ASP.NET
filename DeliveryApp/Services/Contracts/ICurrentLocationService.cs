using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface ICurrentLocationService
    {
        CurrentLocation GetDeliveryManCurrentLocation(int deliveryManId);
        CurrentLocation AddDeliveryManCurrentLocation(CurrentLocation currentLocation);
        CurrentLocation UpdateDeliveryManCurrentLocation(CurrentLocation currentLocation);
        CurrentLocation DeleteDeliveryManCurrentLocation(CurrentLocation currentLocation);
    }
}
