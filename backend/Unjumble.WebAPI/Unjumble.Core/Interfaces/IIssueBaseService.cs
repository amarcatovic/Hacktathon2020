using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Core.Interfaces
{
    public interface IIssueBaseService
    {
        Task AddIssueBase(IssueBaseCreateDto issueBaseCreateDto);

        Task AddIssueBaseComment(IssueBaseCommentCreateDto issueBaseCommentCreateDto);

        Task<List<IssueBaseReturnDto>> GetIssueBasses(string companyId);

        Task<List<IssueBaseReturnDto>> GetIssueBassesByName(string companyId, string searchTerm);
    }
}
