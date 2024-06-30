using AutoMapper;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Cart;
using Cooklee.Data.Entities.Favourite;
using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Entities.Order;
using CookLeeProject.Data.Entities;
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
.ForMember(d => d.FirstName, o => o.MapFrom(S => S.FirstName))

            .ForMember(d => d.LastName, o => o.MapFrom(S => S.LastName))
            .ForMember(d => d.Email, o => o.MapFrom(S => S.Email))
             .ForMember(d => d.ImgURL, o => o.MapFrom(S => S.ImgURL))
             .ForMember(d => d.PhoneNumber, o => o.MapFrom(S => S.PhoneNumber))
              .ForMember(d => d.Address, o => o.MapFrom(S => S.Address));

            CreateMap<ClientProfileDto, Client>();

            CreateMap<ChefPage, ChefPageDto>()
            .ForMember(d => d.DisplayName, o => o.MapFrom(S => S.DisplayName))
             .ForMember(d => d.Id, o => o.MapFrom(S => S.Id));
            CreateMap<ChefPageDto, ChefPage>();

            CreateMap<ClientCartDto, ClientCart>();
            CreateMap<CartItemDto, CartItem>();

            CreateMap<ClientFavouriteDto, ClientFavourite>();
            CreateMap<FavouriteItemDto, FavouriteItem>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();

            CreateMap<Order, OrderDto>();
                
            




            CreateMap<ShipmentDetailsDto, ShipmentDetails>();

            CreateMap<Meal, MealDto>()
                .ForMember(d => d.chefPageName, m => m.MapFrom(m => m.ChefPage.DisplayName));

			CreateMap<MealDto, Meal>()
   .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.MealName))
   .ForMember(dest => dest.MealDescription, opt => opt.MapFrom(src => src.MealDescription))
   .ForMember(dest => dest.IsHealthy, opt => opt.MapFrom(src => src.IsHealthy))
   .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
   .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
   .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
   .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
   .ForMember(dest => dest.ChefPageId, opt => opt.MapFrom(src => src.ChefPageId))
   .ForMember(dest => dest.ChefPage, opt => opt.MapFrom(src => src.ChefPageId));

            CreateMap<Meal, MealDto>();

            CreateMap<AddMealDto, Meal>();
            CreateMap<AddMealDto, Meal>()
           .ForMember(dest => dest.ChefPageId, opt => opt.MapFrom(src => src.ChefPageId));
		   
		  

            CreateMap<SpecialMeal, SpecialMealDto>()
                //.ForMember(d => d.ChefPage, o => o.MapFrom(src => src.ChefPage.DisplayName))
                .ForMember(d => d.Client, o => o.MapFrom(src => src.Client.FirstName)).
                ForMember(d => d.id, o => o.MapFrom(src => src.Id));
            //.ReverseMap();
            //            CreateMap<SpecialMeal, SpecialMealDto>()
            //.ForMember(d => d.ChefPageName, o => o.MapFrom(src => src.ChefPage.DisplayName))
            //.ForMember(d => d.ClientName, o => o.MapFrom(src => src.Client.FirstName));

            CreateMap<SpecialMealDto, SpecialMeal>()
                .ForMember(m => m.Chef, opt => opt.Ignore())
                .ForMember(m => m.Client, opt => opt.Ignore());
        }
    }
}


