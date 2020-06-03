using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface ILocationService
    {
        Location AddLocation(Location newLocation);
        Location GetLocationById(int id);
        Location UpdateLocation(Location newLocation);
        Location DeleteLocation(int id);
    }
}
