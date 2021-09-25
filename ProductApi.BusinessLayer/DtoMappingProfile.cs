using AutoMapper;
using Microsoft.Extensions.Configuration;
using ProductApi.BusinessLayer.Interfaces;
using ProductApi.Core.Dto;
using ProductApi.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.BusinessLayer
{
    public class DtoMappingProfile: Profile
    { 
        public DtoMappingProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

             CreateMap<ProductResponseDto, Product>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
                
        }
    }
}
