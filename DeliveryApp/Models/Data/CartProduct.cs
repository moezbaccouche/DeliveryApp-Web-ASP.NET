using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class CartProduct: BaseEntity
    {
        public int ProductId { get; set; }
        public string Amount { get; set; }
        public int ClientId { get; set; }

    }
}
