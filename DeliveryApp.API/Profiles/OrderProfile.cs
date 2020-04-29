using AutoMapper;
using DeliveryApp.API.Models.DTO.OrdersDTOs;
using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, ClientPendingOrderDto>();
        }
    }
}
