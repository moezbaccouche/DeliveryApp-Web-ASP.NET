using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DeliveryApp.API.Models;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Extensions;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

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
        private readonly IWebHostEnvironment _env;
        private readonly IEmailSender _emailSender;
        private readonly IEmailSenderService emailSenderService;
        private readonly ApplicationSettings _appSettings;

        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;


        public ClientsController(IMapper mapper, IClientService clientService, ILocationService locationService,
            IFavoritesService favoritesService, IOrderService orderService,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment env, IEmailSender emailSender, IEmailSenderService emailSenderService,
            IOptions<ApplicationSettings> appSettings)
        {
            _mapper = mapper;
            this.clientService = clientService;
            this.locationService = locationService;
            this.favoritesService = favoritesService;
            this.orderService = orderService;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _emailSender = emailSender;
            this.emailSenderService = emailSenderService;
            _appSettings = appSettings.Value;
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
        [HttpPost("edit")]
        public async Task<ActionResult<ClientForProfileDto>> UpdateClient(ClientForProfileDto editedClient)
        {
            var client = clientService.GetClientById(editedClient.Id);
            if (client == null)
            {
                return NotFound();
            }


            //Consider the case where the image has been updated
            var imagePath = client.PicturePath;
            var imageBase64 = client.ImageBase64;
            if (editedClient.ImageBase64.ToString() != client.ImageBase64.ToString())
            {
                //Transform the image base64 String
                ImageModel uploadedImage = FileUploader.Base64ToImage(editedClient.ImageBase64String, "ClientsPictures");
                imagePath = uploadedImage.Path;
                imageBase64 = uploadedImage.ImageBytes;
            }

            //Update the location
            var location = locationService.GetLocationById(editedClient.Location.Id);
            if (location == null)
            {
                return NotFound();
            }

            var newLocation = locationService.UpdateLocation(new Location
            {
                Id = location.Id,
                Address = editedClient.Location.Address,
                City = editedClient.Location.City,
                ZipCode = editedClient.Location.ZipCode,
                Lat = editedClient.Location.Lat,
                Long = editedClient.Location.Long
            });



            var hasValidatedEmail = client.HasValidatedEmail;
            //Consider the case where the email has been updated
            if (client.Email != editedClient.Email)
            {
                //Update the email in asp identity
                var user = await _userManager.FindByIdAsync(client.IdentityId);
                user.Email = editedClient.Email;
                user.UserName = editedClient.Email;
                await _userManager.UpdateAsync(user);

                client.Email = editedClient.Email;

                //Set the column HasValidatedEmail to False
                hasValidatedEmail = false;

                //Send verification email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                SendVerificationEmail(client, user.Id, code, true);
            }

            var newClient = clientService.UpdateClient(new Client
            {
                Id = editedClient.Id,
                FirstName = editedClient.FirstName,
                LastName = editedClient.LastName,
                DateOfBirth = editedClient.DateOfBirth,
                Email = editedClient.Email,
                IdentityId = client.IdentityId,
                ImageBase64 = imageBase64,
                Location = newLocation,
                Phone = editedClient.Phone,
                PicturePath = imagePath,
                HasValidatedEmail = hasValidatedEmail
            });

            return Ok(_mapper.Map<ClientForProfileDto>(newClient));
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

                    //Transform the image base64 String
                    ImageModel uploadedImage = FileUploader.Base64ToImage(newClient.ImageBase64String, "ClientsPictures");

                    //Create the client entity
                    var entityClient = new Client
                    {
                        IdentityId = user.Id,
                        Email = newClient.Email,
                        FirstName = newClient.FirstName,
                        LastName = newClient.LastName,
                        Phone = newClient.Phone,
                        DateOfBirth = newClient.DateOfBirth,
                        ImageBase64 = uploadedImage.ImageBytes,
                        PicturePath = uploadedImage.Path,
                        Location = location,
                        HasValidatedEmail = false
                    };

                    //Insert the new user in the DB
                    var addedClient = clientService.AddClient(entityClient);

                    //Send the verification email
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    SendVerificationEmail(addedClient, user.Id, code, false);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [EnableCors("AllowAll")]
        [HttpPost("loginClient")]
        public async Task<ActionResult> LoginClient(ClientCredentialsForLoginDto credentials)
        {
            var user = await _userManager.FindByNameAsync(credentials.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, credentials.Password))
            {
                var client = clientService.GetClientByIdentityId(user.Id);
                if (!client.HasValidatedEmail)
                {
                    return BadRequest(new
                    {
                        message = "Votre compte n'a pas encore été activé ! Vérifiez votre boite Emails."
                    });
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(365),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Email ou mot de passe incorrects" });
            }
        }

        [EnableCors("AllowAll")]
        [HttpPost("resetPassword")]
        public async Task<Object> ForgotPassword(EmailForForgotPasswordDto emailDto)
        {
            var user = await _userManager.FindByEmailAsync(emailDto.Email);
            if(user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var callBackUrl = "http://192.168.1.4:51044/api/resetPassword?userId=" + user.Id + "&token=" + token;

                string parent = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
                string path;
                
                path = Path.Combine(parent, "DeliveryApp\\wwwroot\\Templates\\EmailTemplates\\ResetPasswordEmail.html");
                

                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(path))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }

                string messageBody = string.Format(
                    builder.HtmlBody,
                    callBackUrl
                    );

                await emailSenderService.SendResetPasswordEmail(emailDto.Email, messageBody);
            }
            return Ok(new { message = "Email envoyé." });
        }

        private async void SendVerificationEmail(Client newClient, string userId, string code, bool editedEmail)
        {
            var callBackUrl = "https://localhost:44352/Account/ConfirmClientEmail?userId=" + userId + "&code=" + code;

            string parent = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            string path;
            if (editedEmail)
            {
                path = Path.Combine(parent, "DeliveryApp\\wwwroot\\Templates\\EmailTemplates\\EditedEmailAddress.html");
            }
            else
            {
                path = Path.Combine(parent, "DeliveryApp\\wwwroot\\Templates\\EmailTemplates\\ConfirmRegistration.html");
            }

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(path))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(
                builder.HtmlBody,
                newClient.FirstName + " " + newClient.LastName,
                callBackUrl
                );

            await emailSenderService.SendClientConfirmationEmail(
                newClient.Email,
                newClient.FirstName + " " + newClient.LastName,
                messageBody
                );
        }
    }
}