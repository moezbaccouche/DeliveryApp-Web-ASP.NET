using AutoMapper;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientForProfileDto>();
            CreateMap<UserForCreationDto, Client>();
            CreateMap<Client, ClientForCartDto>();
            CreateMap<Client, ClientForPendingOrdersDto>()
                .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(
                    src => src.FirstName + " " + src.LastName
                    ));
        }
    }
}
