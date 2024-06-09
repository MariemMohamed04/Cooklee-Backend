using AutoMapper;
using Cooklee.Core.DTOs;
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
                .ForMember(d => d.DisplayName, o => o.MapFrom(S => S.DisplayName))
                .ForMember(d => d.Email, o => o.MapFrom(S => S.Email))
                .ForMember(d => d.Password, o => o.MapFrom(S => S.PasswordHash));

            CreateMap<AppUser, UserToReturnDto>()

            .ForMember(d => d.DisplayName, o => o.MapFrom(S => S.DisplayName))
             .ForMember(d => d.Email, o => o.MapFrom(S => S.Email))
              .ForMember(d => d.Password, o => o.MapFrom(S => S.PasswordHash));
        }
    }
}
