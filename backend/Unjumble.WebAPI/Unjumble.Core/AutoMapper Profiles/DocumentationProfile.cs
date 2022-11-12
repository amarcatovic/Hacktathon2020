using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Unjumble.Core.Models.Domain;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Core.AutoMapper_Profiles
{
    public class DocumentationProfile : Profile
    {
        public DocumentationProfile()
        {
            CreateMap<DocumentationPiece, DocumentationPieceReturnDto>();
        }
    }
}
