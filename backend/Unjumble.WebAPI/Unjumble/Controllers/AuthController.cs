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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authentification;

        public AuthController(IAuthService authentification)
        {
            _authentification = authentification;
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthUserAsync([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authentification.AuthenticateUserAsync(loginDto);
                if (result == null)
                    return Unauthorized("Username or password are invalid!");

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetChatUsers(string companyId)
        {
            var result = await _authentification.GetUsersForChat(companyId);

            return Ok(result);
        }
    }
}