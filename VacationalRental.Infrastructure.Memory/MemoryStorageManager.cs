using System.Collections.Generic;

namespace VacationalRental.Infrastructure.Memory
{
    public class MemoryStorageManager : IStorageManager
    {
        private readonly IDictionary<int, VacationRental.Domain.Rental.Models.Rental> _rentals;

        private readonly IDictionary<int, VacationRental.Domain.Booking.Models.Booking> _booking;

        public MemoryStorageManager()
        {
            _rentals = new Dictionary<int, VacationRental.Domain.Rental.Models.Rental>();
            _booking = new Dictionary<int, VacationRental.Domain.Booking.Models.Booking>();
        }

        public IDictionary<int, VacationRental.Domain.Rental.Models.Rental> Rentals => this._rentals;

        public IDictionary<int, VacationRental.Domain.Booking.Models.Booking> Booking => this._booking;

    }
}
