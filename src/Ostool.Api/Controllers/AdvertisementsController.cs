using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostool.Api.Filters;
using Ostool.Application.Features.Advertisements.GetAll;
using Ostool.Application.Features.Advertisements.GetAllByModel;
using Ostool.Application.Features.Advertisements.GetAllByVendor;
using Ostool.Application.Features.Advertisements.GetById;
using Ostool.Application.Features.Advertisements.PostAd;
using Ostool.Infrastructure.Services;

namespace Ostool.Api.Controllers
{
    [Route("ads")]
    [ApiController]
    public class AdvertisementsController : ApiController
    {
        private readonly ISender _mediator;
        private readonly EmailService _emailService;

        public AdvertisementsController(ISender mediator, EmailService emailService)
        {
            _mediator = mediator;
            _emailService = emailService;
        }

        [HttpPost("Add")]
        [Idempotent]
        public async Task<IActionResult> AddAd([FromBody] PostAdCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { result.Value.Id }, result.Value) : Problem(result.Error!);
        }

        [HttpGet("GetAllByModel")]
        public async Task<IActionResult> GetAllByModel([FromQuery] string model, int page)
        {
            var result = await _mediator.Send(new GetAllAdsByModelQuery(model, page));
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }

        [HttpGet("GetAllByVendor")]
        public async Task<IActionResult> GetAllByVendor([FromQuery] string vendor, int page)
        {
            var result = await _mediator.Send(new GetAllByVendorQuery(vendor, page));
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int page)
        {
            var result = await _mediator.Send(new GetAllAdsQuery(page));
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new GetAdByIdQuery(id));
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }

        /*[HttpGet("email")]
        public async Task<IActionResult> Asdd()
        {
            await Task.CompletedTask;
            _emailService.Send();
            return Ok();
        }*/
    }
}