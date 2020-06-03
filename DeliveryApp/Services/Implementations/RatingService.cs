using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<Rating> repoRatings;

        public RatingService(IRepository<Rating> repoRatings)
        {
            this.repoRatings = repoRatings;
        }

        public Rating AddRating(Rating newRating)
        {
            if(newRating != null)
            {
                repoRatings.Insert(newRating);
            }
            return newRating;
        }

        public Rating DeleteRating(Rating rating)
        {
            if(rating != null)
            {
                repoRatings.Delete(rating);
            }

            return rating;
        }

        public Rating EditRating(Rating newRating)
        {
            if(newRating != null)
            {
                repoRatings.Update(newRating);
            }
            return newRating;
        }

        public Rating GetClientRatingForDeliveryMan(int clientId, int deliveryManId)
        {
            var rating = repoRatings.TableNoTracking
                .Where(r => r.IdClient == clientId && r.IdDeliveryMan == deliveryManId)
                .FirstOrDefault();
            return rating;
        }

        public IEnumerable<Rating> GetClientRatings(int clientId)
        {
            var ratings = repoRatings.TableNoTracking
                .Where(r => r.IdClient == clientId)
                .ToList();

            return ratings;
        }

        public IEnumerable<Rating> GetDeliveryManRatings(int deliveryManId)
        {
            var ratings = repoRatings.TableNoTracking
                .Where(r => r.IdDeliveryMan == deliveryManId)
                .ToList();
            return ratings;
        }
    }
}
