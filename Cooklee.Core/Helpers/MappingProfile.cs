using AutoMapper;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities;
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
            // CreateMap<Entity, DTO>
                CreateMap<AppUser, UserDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                //.ForMember(d => d.DisplayName, o => o.MapFrom(S => S.DisplayName))
                .ForMember(d => d.Email, o => o.MapFrom(S => S.Email))
                .ForMember(d => d.Password, o => o.MapFrom(S => S.PasswordHash));

            CreateMap<AppUser, UserToReturnDto>()

            //.ForMember(d => d.DisplayName, o => o.MapFrom(S => S.DisplayName))
             .ForMember(d => d.Email, o => o.MapFrom(S => S.Email))
              .ForMember(d => d.Password, o => o.MapFrom(S => S.PasswordHash)); //.ForMember(d => d.Address, o => o.MapFrom(S => S.Address));

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

        }
    }
}
