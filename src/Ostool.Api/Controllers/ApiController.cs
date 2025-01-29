using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostool.Application.Helpers;

namespace Ostool.Api.Controllers
{

    [ApiController]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(Error error)
        {
            return Problem(
                statusCode: (int)error.StatusCode,
                title: error.Title,
                detail: error.Message
                );
        }
    }
}