using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Core.Interfaces
{
    public interface ICalendarEventService
    {
        Task AddCalendarEvent(CalendarEventCreateDto calendarEventCreateDto);

        Task UpdateCalendarEvent(int id, CalendarEventUpdateDto calendarEventUpdateDto);

        Task<IEnumerable<CalendarEventReturnDto>> GetAllUserCalendarEvents(string userId);

        Task<IEnumerable<CalendarEventReturnDto>> GetCompanyDisplayCalendar(string companyId);
    }
}
