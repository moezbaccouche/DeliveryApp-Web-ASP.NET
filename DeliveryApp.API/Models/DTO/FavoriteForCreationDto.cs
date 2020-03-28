using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class FavoriteForCreationDto
    {
        public int ClientId { get; set; }
        public int ProductId { get; set; }
    }
}
