using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.AppService.Rental.Services;

namespace VacationRental.Api.Controllers
{
    using VacationRental.AppService.Rental.Models.Requests;

    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalAppService _rentalAppService;

        public RentalsController(IRentalAppService rentalAppService)
        {
            _rentalAppService = rentalAppService;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public RentalViewModel Get(int rentalId)
        {
            var response = _rentalAppService.Get(new GetRentalRequest 
            {
                Id = rentalId
            });

            return new RentalViewModel 
            {
                Id = response.Id,
                Units = response.Units,
                PreparationTimeInDays = response.PreparationTimeInDays
            };
        }

        [HttpPost]
        public ResourceIdViewModel Post(RentalBindingModel model)
        {
            var response = _rentalAppService.Create(new CreateRentalRequest 
            {
                Units = model.Units,
                PreparationTimeInDays = model.PreparationTimeInDays
            });

            return new ResourceIdViewModel 
            {
                Id = response.Id
            };
        }

        [HttpPut]
        [Route("{rentalId:int}")]
        public ResourceIdViewModel Put(int rentalId, [FromBody] RentalBindingModel model)
        {
            var response = _rentalAppService.Update(new UpdateRentalRequest
            {
                RentalId = rentalId,
                Units = model.Units,
                PreparationTimeInDays = model.PreparationTimeInDays
            });

            return new ResourceIdViewModel
            {
                Id = response.Id
            };
        }
    }
}
