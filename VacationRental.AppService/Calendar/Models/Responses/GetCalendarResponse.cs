using System.Collections.Generic;

namespace VacationRental.AppService.Calendar.Models.Responses
{
    public class GetCalendarResponse
    {
        public GetCalendarResponse() 
        {
            this.Dates = new List<CalendarDateModel>();
        }

        public int RentalId { get; set; }
        public List<CalendarDateModel> Dates { get; set; }
        public List<PreparationTimeModel> PreparationTimes { get; set; }
    }
}
