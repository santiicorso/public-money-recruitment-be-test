using VacationRental.AppService.Booking.Models.Requests;
using VacationRental.AppService.Booking.Models.Responses;

namespace VacationRental.AppService.Booking.Services
{
    public interface IBookingAppService
    {
        CreateBookingResponse Create(CreateBookingRequest createBookingRequest);

        GetBookingResponse Get(GetBookingRequest getBookingRequest);
    }
}
