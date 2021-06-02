using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.AppService.Booking.Services;

namespace VacationRental.Api.Controllers
{
    using AppService.Booking.Models.Requests;

    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingAppService _bookingAppService;

        public BookingsController(IBookingAppService bookingAppService)
        {
            _bookingAppService = bookingAppService;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public BookingViewModel Get(int bookingId)
        {
            var response = _bookingAppService.Get(new GetBookingRequest 
            {
                Id = bookingId
            });
            return new BookingViewModel
            {
                Id = response.Id,
                Start = response.Start,
                Nights = response.Nights,
                RentalId = response.RentalId
            };
        }

        [HttpPost]
        public ResourceIdViewModel Post(BookingBindingModel model)
        {
            var response = _bookingAppService.Create(new CreateBookingRequest
            {
                Nights = model.Nights,
                RentalId = model.RentalId,
                Start = model.Start
            });
            return new ResourceIdViewModel
            {
                Id = response.Id
            };
        }
    }
}
