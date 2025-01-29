using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostool.Application.Features.Cars.AddCar;
using Ostool.Application.Features.Cars.UpdateCar;

namespace Ostool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ApiController
    {
        private readonly ISender _mediator;

        public CarsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Get(AddCarCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok("Done") : Problem(result.Error!);
        }

        [HttpPut]
        public async Task<IActionResult> Get(UpdateCarCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok("Done") : Problem(result.Error!);
        }
    }
}