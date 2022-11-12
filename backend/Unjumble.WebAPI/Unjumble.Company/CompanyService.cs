using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unjumble.Core.Interfaces;
using Unjumble.Core.Models;
using Unjumble.Core.Models.Domain;
using Unjumble.Core.Models.Dtos;
using Unjumble.Data;

namespace Unjumble.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CompanyService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddUserToCompany(UserJoinCompanyDto userJoinCompanyDto)
        {
            var companyFromDb = await _context.Companies.FirstOrDefaultAsync(c => c.Code == userJoinCompanyDto.Code);
            if (companyFromDb == null)
                return false;

            var result = new UserCompany
            {
                UserId = userJoinCompanyDto.UserId,
                ComapnyId = companyFromDb.Id,
                IsAproved = true
            };

            await _context.UserCompanies.AddAsync(result);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ApproveUserToCompany(string userId, string companyId)
        {
            var result = await _context.UserCompanies.FirstOrDefaultAsync(c => c.UserId == userId && c.ComapnyId == companyId);
            if (result == null)
                return false;

            result.IsAproved = true;
            _context.UserCompanies.Update(result);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Core.Models.Domain.Company> CrateCompany(CompanyRegisterDto companyRegisterDto)
        {
            var result = new Core.Models.Domain.Company
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = companyRegisterDto.Name,
                Code = companyRegisterDto.Name + companyRegisterDto.City,
                UserOwnerId = companyRegisterDto.UserId,
                City = companyRegisterDto.City
            };

            await _context.Companies.AddAsync(result);

            var userCompany = new UserCompany
            {
                ComapnyId = result.Id,
                UserId = companyRegisterDto.UserId,
                IsAproved = true
            };

            await _context.UserCompanies.AddAsync(userCompany);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<UserCompaniesDto>> GetUserCompanies(string userId)
        {
            var userCompanies = await _context.UserCompanies
                .Include(uc => uc.Company)
                .Where(uc => uc.UserId == userId && uc.IsAproved)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<UserCompaniesDto>>(userCompanies);
            return result;
                
        }
    }
}
