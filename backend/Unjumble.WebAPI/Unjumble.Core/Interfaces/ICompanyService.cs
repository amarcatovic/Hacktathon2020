using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unjumble.Core.Models.Domain;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> CrateCompany(CompanyRegisterDto companyRegisterDto);

        Task<bool> AddUserToCompany(UserJoinCompanyDto userJoinCompanyDto);

        Task<bool> ApproveUserToCompany(string userId, string companyId);

        Task<IEnumerable<UserCompaniesDto>> GetUserCompanies(string userId);
    }
}
