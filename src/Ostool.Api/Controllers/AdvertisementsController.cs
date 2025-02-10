using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostool.Application.Features.Advertisements.GetAll;
using Ostool.Application.Features.Advertisements.GetAllByModel;
using Ostool.Application.Features.Advertisements.GetAllByVendor;
using Ostool.Application.Features.Advertisements.GetById;
using Ostool.Application.Features.Advertisements.PostAd;

namespace Ostool.Api.Controllers
{
    [Route("ads")]
    [ApiController]
    public class AdvertisementsController : ApiController
    {
        private readonly ISender _mediator;

        public AdvertisementsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAd([FromBody] PostAdCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : Problem(result.Error!);
        }

        [HttpGet("GetAllByModel")]
        public async Task<IActionResult> GetAllByModel([FromQuery] string model)
        {
            var result = await _mediator.Send(new GetAllAdsByModelCommand(model));
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }

        [HttpGet("GetAllByVendor")]
        public async Task<IActionResult> GetAllByVendor([FromQuery] string vendor)
        {
            var result = await _mediator.Send(new GetAllByVendorCommand(vendor));
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAdsCommand());
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new GetAdByIdCommand(id));
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }
    }
}