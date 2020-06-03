using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Models.ViewModels;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService clientService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILocationService locationService;
        private readonly IOrderService orderService;
        private readonly IFavoritesService favoritesService;
        private readonly ICartProductService cartProductService;
        private readonly IRatingService ratingService;

        public ClientController(IClientService clientService, UserManager<IdentityUser> userManager,
            ILocationService locationService, IOrderService orderService, IFavoritesService favoritesService,
            ICartProductService cartProductService, IRatingService ratingService)
        {
            this.clientService = clientService;
            _userManager = userManager;
            this.locationService = locationService;
            this.orderService = orderService;
            this.favoritesService = favoritesService;
            this.cartProductService = cartProductService;
            this.ratingService = ratingService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllClients()
        {
            var allClients = clientService.GetAllClients();
            ClientViewModel cvm = new ClientViewModel { AllClients = allClients };

            return View(cvm);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = clientService.GetClientById(id);
            if(client == null)
            {
                return View("NotFound");
            }
            var clientOrders = orderService.GetClientOrders(id);
            foreach(var ord in clientOrders)
            {
                var order = orderService.GetOrderById(ord.Id);
                order.IdClient = 0;

                orderService.EditOrder(order);
            }

            var favorites = favoritesService.GetFavoriteProducts(id);
            foreach(var fav in favorites)
            {
                favoritesService.RemoveProductFromFavorites(fav);
            }

            var ratings = ratingService.GetClientRatings(id);
            foreach(var rating in ratings)
            {
                ratingService.DeleteRating(rating);
            }

            cartProductService.RemoveAllProducts(id);

            clientService.DeleteClient(id);
            locationService.DeleteLocation(client.Location.Id);

            var user = await _userManager.FindByIdAsync(client.IdentityId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }


            TempData["Message"] = "Client supprimé avec succès !";

            return RedirectToAction("AllClients");
            
        }
    }
}