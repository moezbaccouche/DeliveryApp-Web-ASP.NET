using AutoMapper;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Profiles
{
    public class DeliveryManProfile: Profile
    {
        public DeliveryManProfile()
        {
            CreateMap<DeliveryMan, DeliveryManForTrackingPageDto>();
        }
    }
}
