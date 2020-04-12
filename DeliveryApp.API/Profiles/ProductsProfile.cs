using AutoMapper;
using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DeliveryApp.API.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, Models.DTO.ProductForHomeDto>()
                .ForMember(
                dest => dest.ImageBase64,
                opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault().ImageBase64));

            CreateMap<Product, Models.DTO.ProductForCheckout>()
             .ForMember(
             dest => dest.ImageBase64,
             opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault().ImageBase64));
        }
    }
}
