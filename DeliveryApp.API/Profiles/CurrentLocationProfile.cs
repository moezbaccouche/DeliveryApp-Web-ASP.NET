using AutoMapper;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Profiles
{
    public class CurrentLocationProfile: Profile
    {
        public CurrentLocationProfile()
        {
            CreateMap<CurrentLocation, DeliveryManCurrentLocationDto>();
            CreateMap<DeliveryManCurrentLocationDto, CurrentLocation>();
        }
    }
}
