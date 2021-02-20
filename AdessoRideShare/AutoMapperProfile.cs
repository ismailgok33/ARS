using AdessoRideShare.Api.Model.Dtos;
using AdessoRideShare.Data.Model.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddTravelDto, Travel>();
            CreateMap<UpdateTravelDto, Travel>();

            //CreateMap<ShoppingCartItem, GetItemDto>()
            //    .ForMember(
            //    dest => dest.Name,
            //    opt => opt.MapFrom(src => src.Item.Name)
            //    )
            //    .ForMember(
            //     dest => dest.Price,
            //    opt => opt.MapFrom(src => src.Item.Price)
            //    );
            //CreateMap<AddItemToCartDto, Item>();
        }
    }
}
