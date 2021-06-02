using VacationRental.AppService.Rental.Models.Requests;
using VacationRental.AppService.Rental.Models.Responses;

namespace VacationRental.AppService.Rental.Services
{
    public interface IRentalAppService
    {
        CreateRentalResponse Create(CreateRentalRequest createBookingRequest);

        GetRentalResponse Get(GetRentalRequest getBookingRequest);

        UpdateRentalResponse Update(UpdateRentalRequest updateRentalRequest);
    }
}
