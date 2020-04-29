using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Models.DTO.OrdersDTOs
{
    public class ClientOrdersDto
    {
        public List<ClientPendingOrderDto> ClientPendingOrders { get; set; }
            = new List<ClientPendingOrderDto>();
        public List<ClientProcessingOrderDto> ClientProcessingOrders { get; set; }
            = new List<ClientProcessingOrderDto>();
        public List<ClientTreatedOrderDto> ClientTreatedOrders { get; set; }
            = new List<ClientTreatedOrderDto>();
    }
}
