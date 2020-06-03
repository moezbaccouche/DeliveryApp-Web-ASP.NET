using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface IRatingService
    {
        Rating GetClientRatingForDeliveryMan(int clientId, int deliveryManId);
        Rating AddRating(Rating newRating);
        Rating EditRating(Rating newRating);
        IEnumerable<Rating> GetDeliveryManRatings(int deliveryManId);
        IEnumerable<Rating> GetClientRatings(int clientId);
        Rating DeleteRating(Rating rating);
    }
}
