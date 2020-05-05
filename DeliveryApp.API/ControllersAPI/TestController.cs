using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.ControllersAPI
{
    [ApiController]
    [Route("delivery-app/test")]
    public class TestController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public TestController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        [EnableCors("AllowAll")]
        [HttpGet("user")]
        public async Task<Object> GetUser()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            return new { user.Email };
        }
    }
}