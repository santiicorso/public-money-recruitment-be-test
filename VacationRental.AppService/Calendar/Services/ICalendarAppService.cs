using VacationRental.AppService.Calendar.Models.Requests;
using VacationRental.AppService.Calendar.Models.Responses;

namespace VacationRental.AppService.Calendar.Services
{
    public interface ICalendarAppService
    {
        GetCalendarResponse Get(GetCalendarRequest calendarAvailabilityRequest);
    }
}
