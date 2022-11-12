using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unjumble.Core.Interfaces;
using Unjumble.Core.Models.Dtos;
using Unjumble.Data;

namespace Unjumble.CalendarEvent
{
    public class CalendarEventService : ICalendarEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CalendarEventService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddCalendarEvent(CalendarEventCreateDto calendarEventCreateDto)
        {
            var result = _mapper.Map<Core.Models.Domain.CalendarEvent>(calendarEventCreateDto);
            await _context.CalendarEvents.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CalendarEventReturnDto>> GetAllUserCalendarEvents(string userId)
        {
            var calendarEvents = await _context.CalendarEvents.Include(c => c.Company).Where(c => c.UserId == userId).ToListAsync();

            var result = _mapper.Map<IEnumerable<CalendarEventReturnDto>>(calendarEvents);

            return result;
        }

        public async Task<IEnumerable<CalendarEventReturnDto>> GetCompanyDisplayCalendar(string companyId)
        {
            var calendarEvents = await _context.CalendarEvents
                .Include(c => c.User)
                .Where(c => c.CompanyId == companyId && c.IsPublic == true)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<CalendarEventReturnDto>>(calendarEvents);

            return result;
        }

        public async Task UpdateCalendarEvent(int id, CalendarEventUpdateDto calendarEventUpdateDto)
        {
            var result = await _context.CalendarEvents.FirstOrDefaultAsync(c => c.Id == id);
            if(result != null)
            {
                result.Title = calendarEventUpdateDto.Title;
                result.EventEndDate = calendarEventUpdateDto.Krajnji;
                result.EventStartDate = calendarEventUpdateDto.Pocetni;

                _context.CalendarEvents.Update(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}
