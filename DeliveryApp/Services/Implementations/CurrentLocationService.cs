using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{
    public class CurrentLocationService : ICurrentLocationService
    {
        private readonly IRepository<CurrentLocation> repoCurrentLocations;

        public CurrentLocationService(IRepository<CurrentLocation> repoCurrentLocations)
        {
            this.repoCurrentLocations = repoCurrentLocations;
        }

        public CurrentLocation AddDeliveryManCurrentLocation(CurrentLocation currentLocation)
        {
            if (currentLocation != null)
            {
                repoCurrentLocations.Insert(currentLocation);
            }

            return currentLocation;
        }

        public CurrentLocation DeleteDeliveryManCurrentLocation(CurrentLocation currentLocation)
        {
            if (currentLocation != null)
            {
                repoCurrentLocations.Delete(currentLocation);
            }
            return currentLocation;
        }

        public CurrentLocation GetDeliveryManCurrentLocation(int deliveryManId)
        {
            var currentLocation = repoCurrentLocations.TableNoTracking
                .Where(c => c.DeliveryManId == deliveryManId)
                .FirstOrDefault();

            return currentLocation;
        }

        public CurrentLocation UpdateDeliveryManCurrentLocation(CurrentLocation currentLocation)
        {
            if (currentLocation != null)
            {
                repoCurrentLocations.Update(currentLocation);
            }

            return currentLocation;
        }
    }
}
