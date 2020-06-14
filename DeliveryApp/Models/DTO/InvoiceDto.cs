using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.DTO
{
    public class InvoiceDto
    {
        public IEnumerable<OrderProductDto> OrderProducts { get; set; }
        public Client Client { get; set; }
        public Order Order { get; set; }
        public DeliveryInfo DeliveryInfo { get; set; }
        public string SignatureBase64String { get; set; }
    }
}
