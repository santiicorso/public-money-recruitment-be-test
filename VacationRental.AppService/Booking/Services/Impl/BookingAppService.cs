using System;
using VacationRental.AppService.Booking.Models.Requests;
using VacationRental.AppService.Booking.Models.Responses;
using VacationRental.Domain.Booking.Services;

namespace VacationRental.AppService.Booking.Services.Impl
{
    using Domain.Booking.Models;
    using VacationRental.Domain.Rental.Services;

    public class BookingAppService : IBookingAppService
    {
        private readonly IBookingDomainService _bookingDomainService;
        private readonly IRentalDomainService _rentalDomainService;
        public BookingAppService(IBookingDomainService bookingDomainService, IRentalDomainService rentalDomainService) 
        {
            _bookingDomainService = bookingDomainService;
            _rentalDomainService = rentalDomainService;
        }
        public CreateBookingResponse Create(CreateBookingRequest createBookingRequest)
        {
            if (createBookingRequest.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");

            var rentals = _rentalDomainService.GetAll();
            if (!rentals.ContainsKey(createBookingRequest.RentalId))
                throw new ApplicationException("Rental not found");

            var rental = rentals[createBookingRequest.RentalId];
            var bookings = _bookingDomainService.GetAll();

            for (var i = 0; i < createBookingRequest.Nights + rental.PreparationTimeInDays; i++)
            {
                var count = 0;
                foreach (var booking in bookings.Values)
                {
                    if (booking.RentalId == createBookingRequest.RentalId
                        && (booking.Start <= createBookingRequest.Start.Date && booking.Start.AddDays(booking.Nights + rental.PreparationTimeInDays) > createBookingRequest.Start.Date)
                        || (booking.Start < createBookingRequest.Start.AddDays(createBookingRequest.Nights + rental.PreparationTimeInDays) && booking.Start.AddDays(booking.Nights + rental.PreparationTimeInDays) >= createBookingRequest.Start.AddDays(createBookingRequest.Nights + rental.PreparationTimeInDays))
                        || (booking.Start > createBookingRequest.Start && booking.Start.AddDays(booking.Nights + rental.PreparationTimeInDays) < createBookingRequest.Start.AddDays(createBookingRequest.Nights + rental.PreparationTimeInDays)))
                    {
                        count++;
                    }
                }

                if (count >= rentals[createBookingRequest.RentalId].Units)
                    throw new ApplicationException("Not available");
            }

            var newBookingId = _bookingDomainService.Save(new Booking
            {
                Nights = createBookingRequest.Nights,
                RentalId = createBookingRequest.RentalId,
                Start = createBookingRequest.Start.Date
            });
            return new CreateBookingResponse
            {
                Id = newBookingId
            };
        }

        public GetBookingResponse Get(GetBookingRequest getBookingRequest)
        {
            var bookings = _bookingDomainService.GetAll();
            if (!bookings.ContainsKey(getBookingRequest.Id))
                throw new ApplicationException("Booking not found");
            
            return new GetBookingResponse
            {
                Id = bookings[getBookingRequest.Id].Id,
                Nights = bookings[getBookingRequest.Id].Nights,
                RentalId = bookings[getBookingRequest.Id].RentalId,
                Start = bookings[getBookingRequest.Id].Start
            };
        }
    }
}
