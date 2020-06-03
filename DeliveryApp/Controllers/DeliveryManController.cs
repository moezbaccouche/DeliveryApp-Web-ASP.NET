using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Extensions;
using DeliveryApp.Models.Data;
using DeliveryApp.Models.ViewModels;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class DeliveryManController : Controller
    {
        private readonly IDeliveryManService deliveryManService;
        private readonly ILocationService locationService;
        private readonly IDeliveryInfoService deliveryInfoService;
        private readonly ICurrentLocationService currentLocationService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRatingService ratingService;

        public DeliveryManController(IDeliveryManService deliveryManService, ILocationService locationService,
            IDeliveryInfoService deliveryInfoService, ICurrentLocationService currentLocationService,
            UserManager<IdentityUser> userManager, IRatingService ratingService)
        {
            this.deliveryManService = deliveryManService;
            this.locationService = locationService;
            this.deliveryInfoService = deliveryInfoService;
            this.currentLocationService = currentLocationService;
            _userManager = userManager;
            this.ratingService = ratingService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllDeliveryMen()
        {
            var allDeliveryMen = deliveryManService.GetAllDeliveryMen();
            DeliveryMenViewModel deliveryMenViewModel = new DeliveryMenViewModel
            {
                AllDeliveryMen = allDeliveryMen
            };
            return View(deliveryMenViewModel);
        }

        public IActionResult NotValidatedDeliveryMen()
        {
            var notValidatedDeliveryMen = deliveryManService.GetNotValidatedDeliveryMen();
            DeliveryMenViewModel deliveryMenViewModel = new DeliveryMenViewModel
            {
                NotValidatedDeliveryMen = notValidatedDeliveryMen
            };
            return View(deliveryMenViewModel);
        }

        public IActionResult Maps()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditDeliveryMan(int id)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(id);
            DeliveryMenViewModel model = new DeliveryMenViewModel { DeliveryMan = deliveryMan };

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditDeliveryMan(DeliveryMenViewModel dvm, IFormFile file)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(dvm.DeliveryMan.Id);
            var deliveryManLocation = locationService.GetLocationById(dvm.DeliveryMan.Location.Id);

            deliveryMan.FirstName = dvm.DeliveryMan.FirstName;
            deliveryMan.LastName = dvm.DeliveryMan.LastName;
            deliveryMan.Phone = dvm.DeliveryMan.Phone;
            deliveryManLocation.Address = dvm.DeliveryMan.Location.Address;
            deliveryManLocation.City = dvm.DeliveryMan.Location.City;
            deliveryManLocation.ZipCode = dvm.DeliveryMan.Location.ZipCode;

            if(file != null)
            {
                //The picture has been changed
                //Upload the new picture
                var picturePath = FileUploader.UploadImage(file, "DeliveryMenPictures");
                var pictureBytes = FileUploader.FileToBase64(picturePath);

                deliveryMan.PicturePath = picturePath;
                deliveryMan.ImageBase64 = pictureBytes;
            }

            if(deliveryMan.Email != dvm.DeliveryMan.Email)
            {
                //Email has been updated
                var user = await _userManager.FindByIdAsync(deliveryMan.IdentityId);
                if(user != null)
                {
                    user.Email = dvm.DeliveryMan.Email;
                    user.NormalizedEmail = dvm.DeliveryMan.Email.ToUpper();
                    user.UserName = dvm.DeliveryMan.Email;
                    user.NormalizedUserName = dvm.DeliveryMan.Email.ToUpper();
                    deliveryMan.Email = dvm.DeliveryMan.Email;
                    deliveryMan.HasValidatedEmail = true;

                    await _userManager.UpdateAsync(user);
                }
            }

            
            deliveryMan.Location = deliveryManLocation;
            locationService.UpdateLocation(deliveryManLocation);
            deliveryManService.EditDeliveryMan(deliveryMan);
           

            TempData["Message"] = "Livreur modifié avec succès !";

            return RedirectToAction("AllDeliveryMen");
        }

        [HttpGet]
        public IActionResult AcceptDeliveryMan(int id)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(id);
            if(deliveryMan == null)
            {
                return View("NotFound");
            }

            deliveryManService.AcceptDeliveryMan(deliveryMan);

            TempData["Message"] = "Livreur accepté !";

            return RedirectToAction("NotValidatedDeliveryMen");
        }

        [HttpGet]
        public async Task<IActionResult> RejectDeliveryMan(int id)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(id);
            if (deliveryMan == null)
            {
                return View("NotFound");
            }

            deliveryManService.DeleteDeliveryMan(id);

            var user = await _userManager.FindByIdAsync(deliveryMan.IdentityId);
            if(user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            TempData["Message"] = "Livreur refusé !";

            return RedirectToAction("NotValidatedDeliveryMen");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDeliveryMan(int id)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(id);
            if(deliveryMan == null)
            {
                return View("NotFound");
            }

            var deliveryManDeliveredOrdersInfos = deliveryInfoService.GetDeliveryManOrderHistory(id);
            foreach(var info in deliveryManDeliveredOrdersInfos)
            {
                info.IdDeliveryMan = 0;
                deliveryInfoService.EditDeliveryInfo(info);
            }

            var currentLocation = currentLocationService.GetDeliveryManCurrentLocation(id);
            currentLocationService.DeleteDeliveryManCurrentLocation(currentLocation);

            var ratings = ratingService.GetDeliveryManRatings(id);
            foreach(var rating in ratings)
            {
                ratingService.DeleteRating(rating);
            }

            deliveryManService.DeleteDeliveryMan(id);
            locationService.DeleteLocation(deliveryMan.Location.Id);

            var user = await _userManager.FindByIdAsync(deliveryMan.IdentityId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            TempData["Message"] = "Livreur supprimé !";

            return RedirectToAction("AllDeliveryMen");
        }

    }
}