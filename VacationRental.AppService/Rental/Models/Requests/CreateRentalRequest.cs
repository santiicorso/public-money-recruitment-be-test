using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRental.AppService.Rental.Models.Requests
{
    public class CreateRentalRequest
    {
        public int Units { get; set; }

        public int PreparationTimeInDays { get; set; }
    }
}
