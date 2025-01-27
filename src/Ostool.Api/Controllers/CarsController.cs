using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostool.Application.Features.Cars.AddCar;

namespace Ostool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
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

            return result.IsSuccess ? Ok("Done") : BadRequest(result.Error);
        }
    }
}
