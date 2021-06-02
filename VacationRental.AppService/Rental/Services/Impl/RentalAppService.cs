using System;

namespace VacationRental.AppService.Rental.Services.Impl
{
    using System.Collections.Generic;
    using System.Linq;
    using VacationRental.AppService.Rental.Models.Requests;
    using VacationRental.AppService.Rental.Models.Responses;
    using VacationRental.Domain.Booking.Services;
    using VacationRental.Domain.Rental.Services;

    public class RentalAppService : IRentalAppService
    {
        private readonly IRentalDomainService _rentalDomainService;
        private readonly IBookingDomainService _bookingDomainService;
        public RentalAppService(IRentalDomainService rentalDomainService, IBookingDomainService bookingDomainService) 
        {
            _rentalDomainService = rentalDomainService;
            _bookingDomainService = bookingDomainService;
        }
        public CreateRentalResponse Create(CreateRentalRequest createBookingRequest)
        {
            var newRentalId = _rentalDomainService.Save(new Domain.Rental.Models.Rental 
            {
                 Units = createBookingRequest.Units,
                 PreparationTimeInDays = createBookingRequest.PreparationTimeInDays
            });

            return new CreateRentalResponse
            {
                Id = newRentalId
            };
        }

        public GetRentalResponse Get(GetRentalRequest getBookingRequest)
        {
            var rentals = _rentalDomainService.GetAll();
            if (!rentals.ContainsKey(getBookingRequest.Id))
                throw new ApplicationException("Rental not found");

            return new GetRentalResponse
            {
               Id = rentals[getBookingRequest.Id].Id,
               Units = rentals[getBookingRequest.Id].Units,
               PreparationTimeInDays = rentals[getBookingRequest.Id].PreparationTimeInDays
            };
        }

        public UpdateRentalResponse Update(UpdateRentalRequest updateRentalRequest)
        {
            var rentals = _rentalDomainService.GetAll();
            if (!rentals.ContainsKey(updateRentalRequest.RentalId))
                throw new ApplicationException("Rental not found");

            var bookings = _bookingDomainService.GetAll().Values.Where(x => x.RentalId == updateRentalRequest.RentalId).OrderBy(x => x.Start).ToList();
            if (CheckOverBookings(bookings, updateRentalRequest.PreparationTimeInDays)) 
            {
                var rental = rentals[updateRentalRequest.RentalId];
                rental.PreparationTimeInDays = updateRentalRequest.PreparationTimeInDays;
                rental.Units = updateRentalRequest.Units;
                _rentalDomainService.Update(rental);
            }

            return new UpdateRentalResponse 
            {
                Id = updateRentalRequest.RentalId
            };
        }

        private bool CheckOverBookings(List<Domain.Booking.Models.Booking> bookings, int preparationTimeInDays) 
        {
            var result = true;
            if (bookings.Count == 1)
                return result;

            for (int i = 1; i < bookings.Count; i++)
            {
                if (bookings[i - 1].Start.AddDays(bookings[i - 1].Nights + preparationTimeInDays) >= bookings[i].Start.Date)
                {
                    result = false;
                    break;
                }  
            }

            return result;
        }
    }
}
