using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unjumble.Core.Interfaces;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCompany([FromBody] CompanyRegisterDto companyRegisterDto)
        {
            try
            {
                var result = await _companyService.CrateCompany(companyRegisterDto);
                if (result != null)
                    return StatusCode(201);

                throw new Exception();
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong, " + e.Message);
            }
        }

        [HttpPut("join")]
        public async Task<IActionResult> AddUserToCompany([FromBody] UserJoinCompanyDto userJoinCompanyDto)
        {
            try
            {
                var result = await _companyService.AddUserToCompany(userJoinCompanyDto);
                if (result)
                    return NoContent();

                throw new Exception();
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPut("confirm/{userId}/{companyId}")]
        public async Task<IActionResult> ConfirmUser(string userId, string companyId)
        {
            try
            {
                var result = await _companyService.ApproveUserToCompany(userId, companyId);
                if (result)
                    return NoContent();

                throw new Exception();
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCompanies(string userId)
        {
            try
            {
                var result = await _companyService.GetUserCompanies(userId);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}