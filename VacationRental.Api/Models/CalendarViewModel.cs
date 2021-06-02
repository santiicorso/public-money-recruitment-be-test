using System.Collections.Generic;

namespace VacationRental.Api.Models
{
    public class CalendarViewModel
    {
        public int RentalId { get; set; }
        public List<CalendarDateViewModel> Dates { get; set; }
        public List<PreparationTimeViewModel> PreparationTimes { get; set; }
    }
}
