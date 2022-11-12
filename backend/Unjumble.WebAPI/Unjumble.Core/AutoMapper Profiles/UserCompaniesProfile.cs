using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Unjumble.Core.Models.Domain;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Core.AutoMapper_Profiles
{
    public class UserCompaniesProfile : Profile
    {
        public UserCompaniesProfile()
        {
            CreateMap<UserCompany, UserCompaniesDto>()
                .ForMember(dest => dest.IsOwner, opt => opt.MapFrom(src => src.Company.UserOwnerId == src.UserId))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Company.Code))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Company.City));

        }
    }
}
