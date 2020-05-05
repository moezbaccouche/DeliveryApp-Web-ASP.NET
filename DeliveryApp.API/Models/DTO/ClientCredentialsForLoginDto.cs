using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class ClientCredentialsForLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
