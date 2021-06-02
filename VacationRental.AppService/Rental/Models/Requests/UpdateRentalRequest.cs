namespace VacationRental.AppService.Rental.Models.Requests
{
    public class UpdateRentalRequest
    {
        public int RentalId { get; set; }

        public int Units { get; set; }

        public int PreparationTimeInDays { get; set; }
    }
}
