using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostool.Api.Filters;
using Ostool.Application.Features.Vendors.AddVendor;
using Ostool.Application.Features.Vendors.DeleteVendor;
using Ostool.Application.Features.Vendors.GetAll;
using Ostool.Application.Features.Vendors.UpdateVendor;

namespace Ostool.Api.Controllers
{
    [Route("vendors")]
    [ApiController]
    public class VendorsController : ApiController
    {
        private readonly ISender _mediator;

        public VendorsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Add")]
        [Idempotent]
        public async Task<IActionResult> AddVendor(AddVendorCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : Problem(result.Error!);
        }

        [HttpDelete("Delete")]
        [Idempotent]
        public async Task<IActionResult> DeleteVendor(DeleteVendorCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : Problem(result.Error!);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> EditVendor(UpdateVendorCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : Problem(result.Error!);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllVendors([FromQuery] int page)
        {
            var result = await _mediator.Send(new GetAllVendorsCommand(page));
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }
    }
}