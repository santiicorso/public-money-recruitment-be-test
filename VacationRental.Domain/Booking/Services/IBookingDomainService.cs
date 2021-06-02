using System.Collections.Generic;

namespace VacationRental.Domain.Booking.Services
{
    using Booking.Models;
    public interface IBookingDomainService
    {
        IDictionary<int, Booking> GetAll();

        int Save(Booking booking);

        void Update(List<Booking> booking);
    }
}
