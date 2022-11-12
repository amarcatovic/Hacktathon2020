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
    public class IssueBaseController : ControllerBase
    {
        private readonly IIssueBaseService _issueBaseService;

        public IssueBaseController(IIssueBaseService issueBaseService)
        {
            _issueBaseService = issueBaseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddIssueBase([FromBody] IssueBaseCreateDto issueBaseCreateDto)
        {
            try
            {
                await _issueBaseService.AddIssueBase(issueBaseCreateDto);
                return StatusCode(201);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("comment")]
        public async Task<IActionResult> AddIssueBaseComment([FromBody] IssueBaseCommentCreateDto issueBaseCommentCreateDto)
        {
            try
            {
                await _issueBaseService.AddIssueBaseComment(issueBaseCommentCreateDto);
                return StatusCode(201);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllIssuesFromCompany(string id)
        {
            try
            {
                var result = await _issueBaseService.GetIssueBasses(id);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("{id}/search/{term}")]
        public async Task<IActionResult> SearchDocs(string id, string term)
        {
            try
            {
                var result = await _issueBaseService.GetIssueBassesByName(id, term);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}