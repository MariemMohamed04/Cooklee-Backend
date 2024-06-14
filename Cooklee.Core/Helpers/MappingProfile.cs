using AutoMapper;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Cart;
using Cooklee.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
                CreateMap<AppUser, UserDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Email, o => o.MapFrom(S => S.Email))
                .ForMember(d => d.Password, o => o.MapFrom(S => S.PasswordHash));

            CreateMap<AppUser, UserToReturnDto>()

             .ForMember(d => d.Email, o => o.MapFrom(S => S.Email))
              .ForMember(d => d.Password, o => o.MapFrom(S => S.PasswordHash));

            CreateMap<Client, ClientProfileDto>()
             .ForMember(d => d.Id, o => o.MapFrom(S => S.Id))
            .ForMember(d => d.DisplayName, o => o.MapFrom(S => S.DisplayName))
            .ForMember(d => d.Email, o => o.MapFrom(S => S.Email))
             .ForMember(d => d.ImgURL, o => o.MapFrom(S => S.ImgURL))
             .ForMember(d => d.PhoneNumber, o => o.MapFrom(S => S.PhoneNumber))
              .ForMember(d => d.Address, o => o.MapFrom(S => S.Address));

            CreateMap<ClientProfileDto, Client>();

            CreateMap<ChefPage, ChefPageDto>()
            .ForMember(d => d.Id, o => o.MapFrom(S => S.Id))
            .ForMember(d => d.DisplayName, o => o.MapFrom(S => S.DisplayName));
       
            CreateMap<ChefPageDto, ChefPage>();

            CreateMap<CustomerCartDto, CustomerCart>();

            CreateMap<CartItemDto, CartItem>();

            CreateMap<SpecialMeal, SpecialMealDto>()
                .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.S_MealName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.MinPrice))
                .ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src => src.MaxPrice))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId));

            CreateMap<SpecialMealDto, SpecialMeal>()
                .ForMember(dest => dest.S_MealName, opt => opt.MapFrom(src => src.MealName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.MinPrice))
                .ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src => src.MaxPrice))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId));


            CreateMap<SpecialMealDto, SpecialMeal>();
        }
    }
}
