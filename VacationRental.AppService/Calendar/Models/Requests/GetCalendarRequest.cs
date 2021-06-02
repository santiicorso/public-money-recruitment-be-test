using System;

namespace VacationRental.AppService.Calendar.Models.Requests
{
    public class GetCalendarRequest
    {
        public int RentalId { get; set; }
        public DateTime Start { get; set; } 
        public int Nights { get; set; }
    }
}
