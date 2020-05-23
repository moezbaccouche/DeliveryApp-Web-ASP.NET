using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.ControllersAPI
{
    [ApiController]
    [Route("delivery-app/deliveryMen")]
    public class DeliveryMenController : ControllerBase
    {
        private readonly IDeliveryManService deliveryMenService;
        private readonly IRatingService ratingService;
        private readonly IClientService clientService;
        private readonly ILocationService locationService;

        public DeliveryMenController(IDeliveryManService deliveryMenService, IRatingService ratingService,
            IClientService clientService, ILocationService locationService)
        {
            this.deliveryMenService = deliveryMenService;
            this.ratingService = ratingService;
            this.clientService = clientService;
            this.locationService = locationService;
        }

        [EnableCors("AllowAll")]
        [HttpGet("{deliveryManId}")]
        public ActionResult<DeliveryManProfileForPopoverDto> GetDeliveryMan(int deliveryManId)
        {
            var deliveryMan = deliveryMenService.GetDeliveryManById(deliveryManId);
            if(deliveryMan == null)
            {
                return NotFound();
            }

            var ratings = ratingService.GetDeliveryManRatings(deliveryManId);
            int sum = 0;
            foreach(var rating in ratings)
            {
                sum += rating.Rate;
            }
            double overall = (double)sum / ratings.Count();

            var deliveryManProfile = new DeliveryManProfileForPopoverDto
            {
                Id = deliveryMan.Id,
                FullName = $"{deliveryMan.FirstName} {deliveryMan.LastName}",
                ImageBase64 = deliveryMan.ImageBase64,
                Email = deliveryMan.Email,
                Phone = deliveryMan.Phone,
                Rating = Math.Round(overall, 2),
                NbRatings = ratings.Count()
            };

            return Ok(deliveryManProfile);
        }

        [EnableCors("AllowAll")]
        [HttpGet("details/{deliveryManId}")]
        public ActionResult<DeliveryManForProfileDto> GetDeliveryManDetails(int deliveryManId)
        {
            var deliveryMan = deliveryMenService.GetDeliveryManById(deliveryManId);
            if(deliveryMan == null)
            {
                return NotFound();
            }

            var ratings = ratingService.GetDeliveryManRatings(deliveryManId);
            int sum = 0;
            foreach (var rating in ratings)
            {
                sum += rating.Rate;
            }

            double overall = (double)sum / ratings.Count();

            var location = locationService.GetLocationById(deliveryMan.Location.Id);

            var deliveryManDetails = new DeliveryManForProfileDto
            {
                Id = deliveryMan.Id,
                FirstName = deliveryMan.FirstName,
                LastName = deliveryMan.LastName,
                DateOfBirth = deliveryMan.DateOfBirth,
                Email = deliveryMan.Email,
                ImageBase64 = deliveryMan.ImageBase64,
                Phone = deliveryMan.Phone,
                Location = location,
                Rating = Math.Round(overall, 2)
            };

            return Ok(deliveryManDetails);
        }

        [EnableCors("AllowAll")]
        [HttpPost("addRating")]
        public ActionResult<Rating> AddNewRate(Rating newRating)
        {
            var client = clientService.GetClientById(newRating.IdClient);
            if(client == null)
            {
                return NotFound();
            }

            var deliveryMan = deliveryMenService.GetDeliveryManById(newRating.IdDeliveryMan);
            if(deliveryMan == null)
            {
                return NotFound();
            }

            var rating = ratingService.AddRating(newRating);
            return Ok(rating);
        }

        [EnableCors("AllowAll")]
        [HttpPost("editRating")]
        public ActionResult<Rating> EditRating(Rating newRating)
        {
            var client = clientService.GetClientById(newRating.IdClient);
            if (client == null)
            {
                return NotFound();
            }

            var deliveryMan = deliveryMenService.GetDeliveryManById(newRating.IdDeliveryMan);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            var rating = ratingService.GetClientRatingForDeliveryMan(newRating.IdClient, newRating.IdDeliveryMan);
            if(rating == null)
            {
                return NotFound();
            }
            rating.Rate = newRating.Rate;

            var editedRating = ratingService.EditRating(rating);
            return Ok(editedRating);
        }
    }
}