using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Extensions;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.ControllersAPI
{
    [ApiController]
    [Route("delivery-app/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientService clientService;
        private readonly ILocationService locationService;
        private readonly IFavoritesService favoritesService;
        private readonly IOrderService orderService;

        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public ClientsController(IMapper mapper, IClientService clientService, ILocationService locationService,
            IFavoritesService favoritesService, IOrderService orderService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _mapper = mapper;
            this.clientService = clientService;
            this.locationService = locationService;
            this.favoritesService = favoritesService;
            this.orderService = orderService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [EnableCors("AllowAll")]
        [HttpGet]
        public ActionResult<IEnumerable<ClientForProfileDto>> GetAllClients()
        {
            var allClients = clientService.GetAllClients();
            return Ok(_mapper.Map<IEnumerable<ClientForProfileDto>>(allClients));
        }

        [EnableCors("AllowAll")]
        [HttpGet("{clientId}", Name = "GetClient")]
        public ActionResult<ClientForProfileDto> GetClient(int clientId)
        {
            var client = clientService.GetClientById(clientId);
            if (client == null)
            {
                return NotFound();
            }

            var clientForProfile = _mapper.Map<ClientForProfileDto>(client);
            var nbDoneOrders = orderService.GetClientNbDeliveredProducts(clientId);
            var nbFavoriteProducts = favoritesService.GetClientNbFavoriteProducts(clientId);

            clientForProfile.NbDoneOrders = nbDoneOrders;
            clientForProfile.NbFavoriteProducts = nbFavoriteProducts;

            return Ok(clientForProfile);
        }

        [EnableCors("AllowAll")]
        [HttpPost("register")]
        public async Task<Object> NewClient([FromBody] ClientForCreationDto newClient)
        {
            var identityUser = new IdentityUser
            {
                Email = newClient.Email,
                UserName = newClient.Email
            };

            try
            {
                var result = await _userManager.CreateAsync(identityUser, newClient.Password);
                if (result.Succeeded)
                {
                    IdentityUser user = _userManager.Users.Where(u => u.Email == newClient.Email).FirstOrDefault();


                    //Insert in the table location
                    var location = locationService.AddLocation(newClient.Location);

                    //Transform the base64String in a file
                    ImageModel uploadedImage = FileUploader.Base64ToImage(newClient.ImageBase64String, "ClientsPictures");

                    //Create the client entity
                    var entityClient = new Client
                    {
                        IdentityId = user.Id,
                        Email = newClient.Email,
                        FirstName = newClient.FirstName,
                        LastName = newClient.LastName,
                        Phone = newClient.Phone,
                        ImageBase64 = uploadedImage.ImageBytes,
                        PicturePath = uploadedImage.Path,
                        Location = location,
                        HasValidatedEmail = false
                    };

                    //Insert the new user in the DB
                    var addedClient = clientService.AddClient(entityClient);

                    //Send the verification email
                    //
                    //
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}