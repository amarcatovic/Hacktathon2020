using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Unjumble.Core.Models.Domain;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Core.AutoMapper_Profiles
{
    public class IssueBaseProfile : Profile
    {
        public IssueBaseProfile()
        {
            CreateMap<IssueBaseCommentCreateDto, IssueBaseComment>();
            CreateMap<IssueBaseCreateDto, IssueBase>();
            CreateMap<IssueBase, IssueBaseReturnDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(src => src.User.PhotoUrl));

            CreateMap<IssueBaseComment, IssueCommentReturnDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(src => src.User.PhotoUrl));
        }
    }
}
