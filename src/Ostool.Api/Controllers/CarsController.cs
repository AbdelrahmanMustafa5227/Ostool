﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ostool.Api.Filters;
using Ostool.Application.Features.Cars.AddCar;
using Ostool.Application.Features.Cars.AddListOfCars;
using Ostool.Application.Features.Cars.DeleteCar;
using Ostool.Application.Features.Cars.GetByBrand;
using Ostool.Application.Features.Cars.UpdateCar;

namespace Ostool.Api.Controllers
{
    [Route("Cars")]
    [ApiController]
    public class CarsController : ApiController
    {
        private readonly ISender _mediator;

        public CarsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Add")]
        [Idempotent]
        public async Task<IActionResult> AddCar(AddCarCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Created() : Problem(result.Error!);
        }

        [HttpPost("AddList")]
        [Idempotent]
        public async Task<IActionResult> AddListOfCar(List<AddCarCommand> cars)
        {
            var result = await _mediator.Send(new AddListOfCarsCommand(cars));

            return result.IsSuccess ? Created() : Problem(result.Error!);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCar(UpdateCarCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok("Done") : Problem(result.Error!);
        }

        [HttpGet("GetByBrand")]
        public async Task<IActionResult> GetByBrand([FromQuery] string brandName)
        {
            var result = await _mediator.Send(new GetByBrandCommand(brandName));

            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }

        [HttpDelete]
        [Idempotent]
        public async Task<IActionResult> Delete([FromQuery] Guid carId)
        {
            var result = await _mediator.Send(new DeleteCarCommand(carId));
            return result.IsSuccess ? NoContent() : Problem(result.Error!);
        }
    }
}