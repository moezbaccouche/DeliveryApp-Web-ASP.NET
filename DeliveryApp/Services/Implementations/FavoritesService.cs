﻿using DeliveryApp.Data;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IRepository<Favorites> repoFavorites;

        public FavoritesService(IRepository<Favorites> repoFavorites)
        {
            this.repoFavorites = repoFavorites;
        }


        public Favorites AddProductToFavorites(Favorites newFavorite)
        {
            repoFavorites.Insert(newFavorite);
            return newFavorite;
        }

        public IEnumerable<Favorites> GetFavoriteProducts(int clientId)
        {
            var favorites = repoFavorites.TableNoTracking
                .Where(f => f.ClientId == clientId)
                .ToList();
            return favorites;
        }

        public bool IsFavoriteProduct(int productId, int clientId)
        {
            Favorites favorites = (from f in repoFavorites.TableNoTracking
                                   where f.ProductId == productId && f.ClientId == clientId
                                   select f)
                                   .FirstOrDefault();

            //If favorite is null this will return false
            return favorites != null;
        }

        public Favorites RemoveProductFromFavorites(Favorites newFavorite)
        {
            Favorites favorite = (from f in repoFavorites.TableNoTracking
                                 where f.ProductId == newFavorite.ProductId && f.ClientId == newFavorite.ClientId
                                 select f).FirstOrDefault();

            if(favorite != null)
            {
                repoFavorites.Delete(favorite);
            }
            return favorite;
        }

        public int GetClientNbFavoriteProducts(int clientId)
        {
            return repoFavorites.TableNoTracking
                .Where(f => f.ClientId == clientId)
                .Count();
        }
    }
}
