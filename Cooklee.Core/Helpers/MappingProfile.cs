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
            .ForMember(d => d.DisplayName, o => o.MapFrom(S => S.DisplayName));
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
            CreateMap<AddMealDto, Meal>();
               /* .ForMember(m => m.MealName, d => d.MapFrom(d => d.MealName))
                .ForMember(m => m.MealDescription, d => d.MapFrom(d => d.MealDescription))
                .ForMember(m => m.IsHealthy, d => d.MapFrom(d => d.IsHealthy))
                .ForMember(m => m.IsSpecial, d => d.MapFrom(d => d.IsSpecial))
                .ForMember(m => m.Price, d => d.MapFrom(d => d.Price))
                .ForMember(m => m.Image, d => d.MapFrom(d => d.Image))
                .ForMember(m => m.Tags, d => d.MapFrom(d => d.tags))
                .ForMember(m => m.ChefPageId,  d => d.MapFrom(d => d.ChefPageId));*/


        }
    }
}
