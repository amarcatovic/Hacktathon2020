using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unjumble.Core.Interfaces;
using Unjumble.Core.Models.Domain;
using Unjumble.Core.Models.Dtos;
using Unjumble.Data;

namespace Unjumble.Documentation
{
    public class DocumentationService : IDocumentationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;

        public DocumentationService(
            ApplicationDbContext context,
            ICloudinaryService cloudinaryService,
            IMapper mapper)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }
        public async Task AddDocumentation(DocumentationCraeteDto documentationCraeteDto)
        {
            var pdfUrl = await _cloudinaryService.UploadFileAsync(documentationCraeteDto.PdfFile);
            var result = new Core.Models.Domain.Documentation
            {
                PdfUrl = pdfUrl,
                StarCount = 0,
                Name = documentationCraeteDto.Name,
                CompanyId = documentationCraeteDto.CompanyId
            };

            await _context.Documentation.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        public async Task AddDocumentationPiece(DocumentationPieceCreateDto documentationPieceCreateDto)
        {
            var result = new DocumentationPiece
            {
                Purpose = documentationPieceCreateDto.Purpose,
                Name = documentationPieceCreateDto.Name,
                DocumentationId = documentationPieceCreateDto.DocumentationId
            };

            await _context.documentationPieces.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDocumentationStars(int id)
        {
            var doc = await _context.Documentation.FirstOrDefaultAsync(d => d.Id == id);
            doc.StarCount++;

            _context.Documentation.Update(doc);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DocumentationReturnDto>> GetDocumentationInfo(string companyId)
        {
            var everyDocument = await _context.Documentation.Where(d => d.CompanyId == companyId).ToListAsync();

            var result = new List<DocumentationReturnDto>();

            foreach(var doc in everyDocument)
            {
                var resultItem = new DocumentationReturnDto
                {
                    Name = doc.Name,
                    PdfUrl = doc.PdfUrl,
                    StarCount = doc.StarCount,
                    Id = doc.Id
                };

                var items = await _context.documentationPieces.Where(dp => dp.DocumentationId == doc.Id).ToListAsync();


                resultItem.Pieces = _mapper.Map<IEnumerable<DocumentationPieceReturnDto>>(items);

                result.Add(resultItem);
            }

            return result;
        }

        public async Task<List<DocumentationReturnDto>> GetDocumentationInfoByName(string companyId, string searchTerm)
        {
            var everyDocument = await _context.Documentation.Where(d => d.CompanyId == companyId && d.Name.ToLower().Contains(searchTerm.ToLower())).ToListAsync();

            var result = new List<DocumentationReturnDto>();

            foreach (var doc in everyDocument)
            {
                var resultItem = new DocumentationReturnDto
                {
                    Name = doc.Name,
                    PdfUrl = doc.PdfUrl,
                    StarCount = doc.StarCount,
                    Id = doc.Id
                };

                var items = await _context.documentationPieces.Where(dp => dp.DocumentationId == doc.Id).ToListAsync();


                resultItem.Pieces = _mapper.Map<IEnumerable<DocumentationPieceReturnDto>>(items);

                result.Add(resultItem);
            }

            return result;
        }
    }
}
