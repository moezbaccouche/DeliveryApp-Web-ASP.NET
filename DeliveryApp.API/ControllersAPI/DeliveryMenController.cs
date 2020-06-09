using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DeliveryApp.API.Models;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Extensions;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationSettings _appSettings;
        private readonly IEmailSenderService emailSenderService;

        public DeliveryMenController(IDeliveryManService deliveryMenService, IRatingService ratingService,
            IClientService clientService, ILocationService locationService, UserManager<IdentityUser> userManager,
            IOptions<ApplicationSettings> appSettings, IEmailSenderService emailSenderService)
        {
            this.deliveryMenService = deliveryMenService;
            this.ratingService = ratingService;
            this.clientService = clientService;
            this.locationService = locationService;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            this.emailSenderService = emailSenderService;
        }

        [EnableCors("AllowAll")]
        [HttpGet("{deliveryManId}")]
        public ActionResult<DeliveryManProfileForPopoverDto> GetDeliveryMan(int deliveryManId)
        {
            var deliveryMan = deliveryMenService.GetDeliveryManById(deliveryManId);
            if (deliveryMan == null)
            {
                return NotFound();
            }
            double deliveryManRating = 0;

            var ratings = ratingService.GetDeliveryManRatings(deliveryManId);
           
            if(ratings.Count() != 0)
            {
                int sum = 0;
                foreach (var rating in ratings)
                {
                    sum += rating.Rate;
                }
                deliveryManRating = Math.Round(((double)sum / ratings.Count()), 2);
            }
            

            var deliveryManProfile = new DeliveryManProfileForPopoverDto
            {
                Id = deliveryMan.Id,
                FullName = $"{deliveryMan.FirstName} {deliveryMan.LastName}",
                ImageBase64 = deliveryMan.ImageBase64,
                Email = deliveryMan.Email,
                Phone = deliveryMan.Phone,
                Rating = deliveryManRating,
                NbRatings = ratings.Count()
            };

            return Ok(deliveryManProfile);
        }

        [EnableCors("AllowAll")]
        [HttpGet("details/{deliveryManId}")]
        public ActionResult<DeliveryManForProfileDto> GetDeliveryManDetails(int deliveryManId)
        {
            var deliveryMan = deliveryMenService.GetDeliveryManById(deliveryManId);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            double deliveryManRating = 0;
            var ratings = ratingService.GetDeliveryManRatings(deliveryManId);
            if (ratings.Count() != 0)
            {
                int sum = 0;
                foreach (var rating in ratings)
                {
                    sum += rating.Rate;
                }

                deliveryManRating = Math.Round(((double)sum / ratings.Count()), 2);
            }

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
                Rating = deliveryManRating
            };

            return Ok(deliveryManDetails);
        }

        [EnableCors("AllowAll")]
        [HttpPost("addRating")]
        public ActionResult<Rating> AddNewRate(Rating newRating)
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
            if (rating == null)
            {
                return NotFound();
            }
            rating.Rate = newRating.Rate;

            var editedRating = ratingService.EditRating(rating);
            return Ok(editedRating);
        }



        [EnableCors("AllowAll")]
        [HttpPost("register")]
        public async Task<Object> NewDeliveryMan([FromBody] UserForCreationDto newDeliveryMan)
        {
            var identityUser = new IdentityUser
            {
                Email = newDeliveryMan.Email,
                UserName = newDeliveryMan.Email
            };

            if (await _userManager.FindByEmailAsync(newDeliveryMan.Email) != null)
            {
                return BadRequest(new { code = "DuplicatedEmail", message = "Cette adresse email est déjà utilisée." });
            }

            try
            {
                var result = await _userManager.CreateAsync(identityUser, newDeliveryMan.Password);
                if (result.Succeeded)
                {
                    IdentityUser user = _userManager.Users.Where(u => u.Email == newDeliveryMan.Email).FirstOrDefault();


                    //Insert in the table location
                    var location = locationService.AddLocation(newDeliveryMan.Location);

                    //Transform the image base64 String
                    ImageModel uploadedImage = FileUploader.Base64ToImage(newDeliveryMan.ImageBase64String, "DeliveryMenPictures");

                    //Create the client entity
                    var entityDeliveryMan = new DeliveryMan
                    {
                        IdentityId = user.Id,
                        Email = newDeliveryMan.Email,
                        FirstName = newDeliveryMan.FirstName,
                        LastName = newDeliveryMan.LastName,
                        Phone = newDeliveryMan.Phone,
                        DateOfBirth = newDeliveryMan.DateOfBirth,
                        ImageBase64 = uploadedImage.ImageBytes,
                        PicturePath = uploadedImage.Path,
                        Location = location,
                        HasValidatedEmail = false,
                        IsValidated = false
                    };

                    //Insert the new user in the DB
                    var addedDeliveryMan = deliveryMenService.AddDeliveryMan(entityDeliveryMan);

                    //Send the verification email
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    SendVerificationEmail(addedDeliveryMan, user.Id, code);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [EnableCors("AllowAll")]
        [HttpPost("loginDeliveryMan")]
        public async Task<ActionResult> LoginDeliveryMan(UserCredentialsForLoginDto credentials)
        {
            var user = await _userManager.FindByNameAsync(credentials.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, credentials.Password))
            {
                var deliveryMan = deliveryMenService.GetDeliveryManByIdentityId(user.Id);
                if (!deliveryMan.HasValidatedEmail)
                {
                    return BadRequest(new
                    {
                        message = "Votre compte n'a pas encore été activé ! Vérifiez votre boite Emails."
                    });
                }

                if (!deliveryMan.IsValidated)
                {
                    return BadRequest(new
                    {
                        message = "Vous n'avez pas encore été accepté par l'administrateur !"
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
                return Ok(new { Token = token, Id = deliveryMan.Id });
            }
            else
            {
                return BadRequest(new { message = "Email ou mot de passe incorrect" });
            }
        }


        [EnableCors("AllowAll")]
        [HttpPost("resetPassword")]
        public async Task<Object> ForgotPassword(EmailForForgotPasswordDto emailDto)
        {

            var ip = UsefulMethods.GetLocalIPAddress();
            var user = await _userManager.FindByEmailAsync(emailDto.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var callBackUrl = "http://" + ip + ":51044/api/resetPassword?userId=" + user.Id + "&token=" + token;

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


        [EnableCors("AllowAll")]
        [HttpPost("resendEmail")]
        public async Task<Object> ResendVerificationEmail(EmailToResendDto emailToResend)
        {
            var user = await _userManager.FindByEmailAsync(emailToResend.Email);
            if (user != null)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var deliveryMan = deliveryMenService.GetDeliveryManByIdentityId(user.Id);
                this.SendVerificationEmail(deliveryMan, user.Id, code);
            }
            return Ok(new { message = "Email envoyé." });
        }

        [EnableCors("AllowAll")]
        [HttpPost("setPlayerId")]
        public ActionResult<DeliveryMan> SetDeliveryManPlayerId(DeliveryManForSettingPlayerIdDto deliveryManDto)
        {
            var deliveryMan = deliveryMenService.GetDeliveryManById(deliveryManDto.DeliveryManId);
            if(deliveryMan == null)
            {
                return NotFound();
            }

            if(deliveryMan.PlayerId == null || deliveryMan.PlayerId != deliveryManDto.PlayerId)
            {
                deliveryMan.PlayerId = deliveryManDto.PlayerId;
                deliveryMenService.EditDeliveryMan(deliveryMan);
            }

            return Ok(deliveryMan);
        }

        [EnableCors("AllowAll")]
        [HttpGet("playersIds")]
        public ActionResult<IEnumerable<string>> GetDeliveryMenPlayerIds()
        {
            var deliveryMenPlayerIds = deliveryMenService.GetAllDeliveryMenPlayerIds();

            return Ok(deliveryMenPlayerIds);
        }


        private async void SendVerificationEmail(DeliveryMan newDeliveryMan, string userId, string code)
        {
            var ip = UsefulMethods.GetLocalIPAddress();
            var callBackUrl = "http://" + ip + ":51044/api/ConfirmDeliveryManEmail?userId=" + userId + "&code=" + code;

            string parent = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            string path;

            path = Path.Combine(parent, "DeliveryApp\\wwwroot\\Templates\\EmailTemplates\\ConfirmRegistration.html");


            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(path))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string messageBody = string.Format(
                builder.HtmlBody,
                newDeliveryMan.FirstName + " " + newDeliveryMan.LastName,
                callBackUrl
                );

            await emailSenderService.SendUserConfirmationEmail(
                newDeliveryMan.Email,
                newDeliveryMan.FirstName + " " + newDeliveryMan.LastName,
                messageBody
                );
        }


    }
}