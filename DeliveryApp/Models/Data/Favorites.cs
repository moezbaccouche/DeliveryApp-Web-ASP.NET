using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class Favorites : BaseEntity
    {
        public int ProductId { get; set; }
        public int ClientId { get; set; }
    }
}
