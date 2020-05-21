using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO
{
    public class ProcessingOrderForDeliveryManDto
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public ClientForPendingOrdersDto Client { get; set; }
        public string Status { get; set; }
    }
}
