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
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarEventService _calendarEventService;

        public CalendarController(ICalendarEventService calendarEventService)
        {
            _calendarEventService = calendarEventService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] CalendarEventCreateDto calendarEventCreateDto)
        {
            try
            {
                await _calendarEventService.AddCalendarEvent(calendarEventCreateDto);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] CalendarEventUpdateDto calendarEventUpdateDto)
        {
            try
            {
                await _calendarEventService.UpdateCalendarEvent(id, calendarEventUpdateDto);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetEvents(string userId)
        {
            try
            {
                var result = await _calendarEventService.GetAllUserCalendarEvents(userId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{companyId}/public")]
        public async Task<IActionResult> GetEventsForCompanyDisplay(string companyId)
        {
            try
            {
                var result = await _calendarEventService.GetCompanyDisplayCalendar(companyId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}