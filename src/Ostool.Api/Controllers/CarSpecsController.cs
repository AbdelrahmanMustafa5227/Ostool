using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostool.Api.Filters;
using Ostool.Application.Features.CarSpecs.AddCarDetails;

namespace Ostool.Api.Controllers
{
    [Route("specs")]
    [ApiController]
    public class CarSpecsController : ApiController
    {
        private readonly ISender _mediator;

        public CarSpecsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Add")]
        [Idempotent]
        public async Task<IActionResult> AddCarDetails(AddCarDetailsCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : Problem(result.Error!);
        }
    }
}