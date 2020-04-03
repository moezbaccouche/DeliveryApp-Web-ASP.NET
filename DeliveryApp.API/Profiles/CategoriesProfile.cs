using AutoMapper;
using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Profiles
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category, Models.DTO.CategoryDto>();
            CreateMap<Category, Models.DTO.CategoryDtoWithBase64>();
        }
    }
}
