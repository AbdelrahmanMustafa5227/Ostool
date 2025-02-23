using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostool.Application.Features.Visitors.GetSavedAds;
using Ostool.Application.Features.Visitors.SaveAdvertisement;
using Ostool.Application.Features.Visitors.UnsaveAdvertisement;

namespace Ostool.Api.Controllers
{
    [Route("visitors")]
    [ApiController]
    public class VisitorsController : ApiController
    {
        private readonly ISender _mediatr;

        public VisitorsController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveAdvertisement([FromBody] SaveAdvertisementCommand command)
        {
            var result = await _mediatr.Send(command);
            return result.IsSuccess ? Ok() : Problem(result.Error!);
        }

        [HttpPost("unsave")]
        public async Task<IActionResult> UnsaveAdvertisement([FromBody] UnsaveAdvertisementCommand command)
        {
            var result = await _mediatr.Send(command);
            return result.IsSuccess ? Ok() : Problem(result.Error!);
        }

        [HttpGet("getSaved")]
        public async Task<IActionResult> GetSavedAdvertisement([FromBody] GetSavedAdsQuery query)
        {
            var result = await _mediatr.Send(query);
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }
    }
}