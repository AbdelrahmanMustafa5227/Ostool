using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostool.Application.Features.Vendors.AddVendor;
using Ostool.Application.Features.Vendors.DeleteVendor;
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
        public async Task<IActionResult> AddVendor(AddVendorCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : Problem(result.Error!);
        }

        [HttpDelete("Delete")]
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
    }
}