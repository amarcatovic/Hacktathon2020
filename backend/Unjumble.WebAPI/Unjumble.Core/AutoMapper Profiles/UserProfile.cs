using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Unjumble.Core.Models;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Core.AutoMapper_Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReturnDto>();
        }
    }
}
