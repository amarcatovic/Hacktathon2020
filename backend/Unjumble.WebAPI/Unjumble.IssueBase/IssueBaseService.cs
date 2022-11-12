using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unjumble.Core.Interfaces;
using Unjumble.Core.Models.Dtos;
using Unjumble.Data;

namespace Unjumble.IssueBase
{
    public class IssueBaseService : IIssueBaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;

        public IssueBaseService(
            ApplicationDbContext context,
            ICloudinaryService cloudinaryService,
            IMapper mapper)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }

        public async Task AddIssueBase(IssueBaseCreateDto issueBaseCreateDto)
        {
            var result = _mapper.Map<Core.Models.Domain.IssueBase>(issueBaseCreateDto);

            await _context.IssueBases.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        public async Task AddIssueBaseComment(IssueBaseCommentCreateDto issueBaseCommentCreateDto)
        {
            var result = _mapper.Map<Core.Models.Domain.IssueBaseComment>(issueBaseCommentCreateDto);

            await _context.IssueBaseComments.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        public async Task<List<IssueBaseReturnDto>> GetIssueBasses(string companyId)
        {
            var issues = await _context.IssueBases.Include(i => i.User).Where(d => d.CompanyId == companyId).ToListAsync();

            var result = new List<IssueBaseReturnDto>();

            foreach (var issue in issues)
            {
                var resultItem = _mapper.Map<IssueBaseReturnDto>(issue);

                var items = await _context.IssueBaseComments.Where(dp => dp.IssueBaseId == issue.Id).ToListAsync();


                resultItem.Comments = _mapper.Map<IEnumerable<IssueCommentReturnDto>>(items);

                result.Add(resultItem);
            }

            return result;
        }

        public async Task<List<IssueBaseReturnDto>> GetIssueBassesByName(string companyId, string searchTerm)
        {
            var issues = await _context.IssueBases.Include(i => i.User).Where(d => d.CompanyId == companyId && d.Title.ToLower().Contains(searchTerm.ToLower())).ToListAsync();

            var result = new List<IssueBaseReturnDto>();

            foreach (var issue in issues)
            {
                var resultItem = _mapper.Map<IssueBaseReturnDto>(issue);

                var items = await _context.IssueBaseComments.Where(dp => dp.IssueBaseId == issue.Id).ToListAsync();


                resultItem.Comments = _mapper.Map<IEnumerable<IssueCommentReturnDto>>(items);

                result.Add(resultItem);
            }

            return result;
        }
    }
}
