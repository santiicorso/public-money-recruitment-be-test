using System.Collections.Generic;
using VacationRental.Domain.Rental.Services;

namespace VacationalRental.Infrastructure.Memory.Rental
{
    using VacationRental.Domain.Rental.Models;
    public class RentalDomainService : IRentalDomainService
    {
        private readonly IStorageManager _storageManager;
        public RentalDomainService(IStorageManager storageManager)
        {
            _storageManager = storageManager;
        }

        public IDictionary<int, Rental> GetAll()
        {
            return _storageManager.Rentals;
        }

        public int Save(Rental rental)
        {
            var newId = _storageManager.Rentals.Keys.Count + 1;
            rental.Id = newId;
            _storageManager.Rentals.Add(newId, rental);
            return newId;
        }
        public void Update(Rental rental)
        {
            if (_storageManager.Rentals.ContainsKey(rental.Id))
                _storageManager.Rentals[rental.Id] = rental;
        }
    }
}
