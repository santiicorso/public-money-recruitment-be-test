using System.Collections.Generic;
using VacationRental.Domain.Booking.Services;

namespace VacationalRental.Infrastructure.Memory.Booking
{
    using VacationRental.Domain.Booking.Models;
    public class BookingDomainService : IBookingDomainService
    {
        private readonly IStorageManager _storageManager;
        public BookingDomainService(IStorageManager storageManager) 
        {
            _storageManager = storageManager;
        }
        public IDictionary<int, Booking> GetAll()
        {
            return _storageManager.Booking;
        }

        public int Save(Booking booking)
        {
            var newId = _storageManager.Booking.Keys.Count + 1 ;
            booking.Id = newId;
            _storageManager.Booking.Add(newId, booking);

            return newId;
        }

        public void Update(List<Booking> booking)
        {
            booking.ForEach(x => 
            {
                if (_storageManager.Booking.ContainsKey(x.Id))
                    _storageManager.Booking[x.Id] = x;
            });
        }
    }
}
