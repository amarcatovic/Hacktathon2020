using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unjumble.Core.Models.Domain;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Core.Interfaces
{
    public interface IDocumentationService
    {
        Task AddDocumentation(DocumentationCraeteDto documentationCraeteDto);

        Task UpdateDocumentationStars(int id);

        Task AddDocumentationPiece(DocumentationPieceCreateDto documentationPieceCreateDto);

        Task<List<DocumentationReturnDto>> GetDocumentationInfo(string companyId);

        Task<List<DocumentationReturnDto>> GetDocumentationInfoByName(string companyId, string searchTerm);
    }
}
