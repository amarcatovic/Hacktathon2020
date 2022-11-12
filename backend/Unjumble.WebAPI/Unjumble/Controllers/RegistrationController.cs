using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unjumble.Core.Interfaces;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        /// <summary>
        /// Gets user registration request, and stores him in the database. Sends him confirmation email
        /// </summary>
        /// <param name="registrationDto">Registration data</param>
        /// <returns>201 if okay, 400 if something goes wrong</returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Registration cannot be compleated because the send data is invalid!");

            try
            {
                var isRegistred = await _registrationService.RegisterUserAsync(registrationDto);
                if (isRegistred)
                    return StatusCode(201);

                throw new Exception();
            }
            catch (Exception e)
            {
                return BadRequest("Registration cannot be compleated because something went wrong: " + e.Message);
            }
        }


        [HttpPut("activate/{code}")]
        public async Task<IActionResult> ActivateUserAccountAsync(string code)
        {
            try
            {
                var isActivated = await _registrationService.ActivateUserAccountAsync(code);
                if (isActivated)
                    return NoContent();

                throw new Exception();
            }
            catch (Exception e)
            {
                return BadRequest("There was an error activating user account: " + e.Message);
            }
        }
    }
}