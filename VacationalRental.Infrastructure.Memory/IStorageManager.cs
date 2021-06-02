using System.Collections.Generic;

namespace VacationalRental.Infrastructure.Memory
{
    public interface IStorageManager
    {
        IDictionary<int, VacationRental.Domain.Rental.Models.Rental> Rentals { get; }
        IDictionary<int, VacationRental.Domain.Booking.Models.Booking> Booking { get; }
    }
}
