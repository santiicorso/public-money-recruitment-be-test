namespace VacationRental.Domain.Rental.Services
{
    using Rental.Models;
    using System.Collections.Generic;

    public interface IRentalDomainService
    {
        IDictionary<int, Rental> GetAll();
        int Save(Rental rental);
        void Update(Rental rental);
    }
}
