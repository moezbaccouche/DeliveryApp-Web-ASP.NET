using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface IFavoritesService
    {
        Favorites AddProductToFavorites(Favorites newFavorite);
        Favorites RemoveProductFromFavorites(Favorites newFavorite);
        IEnumerable<Favorites> GetFavoriteProducts(int clientId);
    }
}
