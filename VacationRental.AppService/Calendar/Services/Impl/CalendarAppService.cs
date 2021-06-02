using System;
using System.Collections.Generic;
using VacationRental.AppService.Calendar.Models;
using VacationRental.AppService.Calendar.Models.Requests;
using VacationRental.AppService.Calendar.Models.Responses;
using VacationRental.Domain.Booking.Services;
using VacationRental.Domain.Rental.Services;

namespace VacationRental.AppService.Calendar.Services.Impl
{
    public class CalendarAppService : ICalendarAppService
    {
        private readonly IRentalDomainService _rentalDomainService;
        private readonly IBookingDomainService _bookingDomainService;
        public CalendarAppService(IRentalDomainService rentalDomainService, IBookingDomainService bookingDomainService) 
        {
            _rentalDomainService = rentalDomainService;
            _bookingDomainService = bookingDomainService;
        }
        public GetCalendarResponse Get(GetCalendarRequest calendarAvailabilityRequest)
        {
            if (calendarAvailabilityRequest.Nights < 0)
                throw new ApplicationException("Nights must be positive");
            var rentals = _rentalDomainService.GetAll();
            if (!rentals.ContainsKey(calendarAvailabilityRequest.RentalId))
                throw new ApplicationException("Rental not found");

            var result = new GetCalendarResponse
            {
                RentalId = calendarAvailabilityRequest.RentalId,
                Dates = new List<CalendarDateModel>(),
                PreparationTimes = new List<PreparationTimeModel> 
                {
                    new PreparationTimeModel
                    {
                        Unit = rentals[calendarAvailabilityRequest.RentalId].PreparationTimeInDays
                    }
                }
            };
            for (var i = 0; i < calendarAvailabilityRequest.Nights; i++)
            {
                var date = new CalendarDateModel
                {
                    Date = calendarAvailabilityRequest.Start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingModel>()
                };

                foreach (var booking in _bookingDomainService.GetAll().Values)
                {
                    if (booking.RentalId == calendarAvailabilityRequest.RentalId
                        && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingModel { Id = booking.Id });
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
    }
}
