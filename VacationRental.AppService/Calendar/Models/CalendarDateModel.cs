using System;
using System.Collections.Generic;

namespace VacationRental.AppService.Calendar.Models
{
    public class CalendarDateModel
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingModel> Bookings { get; set; }
    }
}
