using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.AppService.Calendar.Services;

namespace VacationRental.Api.Controllers
{
    using AppService.Calendar.Models.Requests;

    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarAppService _calendarAvailabilityAppService;
        public CalendarController(
            ICalendarAppService calendarAvailabilityAppService)
        {
            _calendarAvailabilityAppService = calendarAvailabilityAppService;
        }

        [HttpGet]
        public CalendarViewModel Get(int rentalId, DateTime start, int nights)
        {
            var response = _calendarAvailabilityAppService.Get(new GetCalendarRequest 
            {
                Nights = nights,
                RentalId = rentalId,
                Start = start
            });

            var result = new CalendarViewModel 
            {
                RentalId = response.RentalId,
            };
            var preparationTimes = new List<PreparationTimeViewModel>();
            response.PreparationTimes.ForEach(preparationTime =>
            {
                preparationTimes.Add(new PreparationTimeViewModel { Unit = preparationTime .Unit});
            });
            result.PreparationTimes = preparationTimes;
            var calendarDates = new List<CalendarDateViewModel>();
            response.Dates.ForEach(date => 
            {
                var calendarDate = new CalendarDateViewModel { Bookings = new List<CalendarBookingViewModel>()};
                calendarDate.Date = date.Date;
                date.Bookings.ForEach(booking => 
                {
                    calendarDate.Bookings.Add(new CalendarBookingViewModel
                    {
                        Id = booking.Id
                    });
                });
                calendarDates.Add(calendarDate);
            });
            result.Dates = calendarDates;
            return result;
        }
    }
}
