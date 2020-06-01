using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class ClientForSettingPlayerIdDto
    {
        public int ClientId { get; set; }
        public string PlayerId { get; set; }
    }
}
