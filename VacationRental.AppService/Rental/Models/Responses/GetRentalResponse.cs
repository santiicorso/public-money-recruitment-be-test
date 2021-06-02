namespace VacationRental.AppService.Rental.Models.Responses
{
    public class GetRentalResponse
    {
        public int Id { get; set; }
        public int Units { get; set; }

        public int PreparationTimeInDays { get; set; }
    }
}
