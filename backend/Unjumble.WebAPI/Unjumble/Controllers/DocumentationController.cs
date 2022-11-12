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
    public class DocumentationController : ControllerBase
    {
        private readonly IDocumentationService _documentationService;

        public DocumentationController(IDocumentationService documentationService)
        {
            _documentationService = documentationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocumentation([FromForm] DocumentationCraeteDto documentationCraeteDto)
        {
            try
            {
                await _documentationService.AddDocumentation(documentationCraeteDto);
                return StatusCode(201);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("piece")]
        public async Task<IActionResult> AddDocumentationPiece([FromBody] DocumentationPieceCreateDto documentationPieceCreateDto)
        {
            try
            {
                await _documentationService.AddDocumentationPiece(documentationPieceCreateDto);
                return StatusCode(201);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPut("star/{id}")]
        public async Task<IActionResult> StarDocumentation(int id)
        {
            try
            {
                await _documentationService.UpdateDocumentationStars(id);
                return NoContent();
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllDocs(string id)
        {
            try
            {
                var result = await _documentationService.GetDocumentationInfo(id);
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
                var result = await _documentationService.GetDocumentationInfoByName(id, term);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}