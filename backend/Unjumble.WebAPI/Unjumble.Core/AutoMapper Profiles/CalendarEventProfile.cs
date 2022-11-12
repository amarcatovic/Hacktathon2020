using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Unjumble.Core.Models.Domain;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Core.AutoMapper_Profiles
{
    public class CalendarEventProfile : Profile
    {
        public CalendarEventProfile()
        {
            CreateMap<CalendarEvent, CalendarEventReturnDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.UserPhotoUrl, opt => opt.MapFrom(src => src.User.PhotoUrl));
            CreateMap<CalendarEventCreateDto, CalendarEvent>();
        }
    }
}
