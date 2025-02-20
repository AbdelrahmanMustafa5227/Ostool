using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Ostool.Application.Features.Auth.GoogleRegister;
using Ostool.Application.Features.Auth.LocalLogin;
using Ostool.Application.Features.Auth.LocalRefresh;
using Ostool.Application.Features.Auth.LocalRegister;
using Ostool.Application.Features.Auth.LocalRevoke;
using Ostool.Infrastructure.Authentication;
using Ostool.Infrastructure.Persistence;
using System.Data;
using System.Text.Json;

namespace Ostool.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly ISender _mediator;

        public AuthController(AppDbContext appDbContext, IDataProtectionProvider dataProtectionProvider, ISender mediator)
        {
            _appDbContext = appDbContext;
            _dataProtectionProvider = dataProtectionProvider;
            _mediator = mediator;
        }

        [HttpPost("LocalAuth-Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LocalLoginCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }

        [HttpPost("LocalAuth-Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] LocalRegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : Problem(result.Error!);
        }

        [HttpPost("LocalAuth-Refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] LocalRefreshCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : Problem(result.Error!);
        }

        [HttpPost("LocalAuth-Revoke")]
        [Authorize(Policy = "Local")]
        public async Task<IActionResult> Revoke([FromBody] LocalRevokeCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : Problem(result.Error!);
        }

        [HttpGet("GoogleAuth")]
        public async Task<IActionResult> Register([FromQuery] int role = 0)
        {
            await Task.CompletedTask;
            var props = new AuthenticationProperties
            {
                RedirectUri = $"/auth/GetClaims",
            };
            props.Items["role"] = role.ToString() ?? "0";
            return Challenge(props, authenticationSchemes: "Google");
        }


        [HttpGet("GoogleCallback")]
        [Authorize(Policy = "Google")]
        public async Task<IActionResult> CompleteGoogleRegisteration([FromQuery] string code, int role)
        {
            var protector = _dataProtectionProvider.CreateProtector("claims");
            var tempUser = JsonSerializer.Deserialize<GoogeUserInfo>(protector.Unprotect(code));

            var command = new GoogleRegisterCommand(tempUser!.Id, tempUser.Email, tempUser.Name, role);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? RedirectToAction(nameof(GetUserClaims)) : Problem(result.Error!);
        }

        [HttpGet("GoogleAuthError")]
        public IActionResult Error([FromQuery] string message)
        {
            return Problem(message);
        }

        [HttpGet("GetClaims")]
        [Authorize(Policy = "Google")]
        [Authorize(Policy = "Local")]
        public async Task<IActionResult> GetUserClaims()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var claims = HttpContext.User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }




        //[HttpPost("revoke")]
        //[Authorize(Policy = "Google-Auth")]
        //public async Task<IActionResult> Revoke()
        //{
        //    var client = new HttpClient();
        //    var accessToken = await HttpContext.GetTokenAsync("access_token");
        //    var request = new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/revoke")
        //    {
        //        Content = new FormUrlEncodedContent(new Dictionary<string, string>
        //        {
        //            { "token",accessToken! }
        //        })
        //    };

        //    var response = await client.SendAsync(request);
        //    return Ok(response.IsSuccessStatusCode); // True if revoked successfully
        //}


        //[HttpGet("authorize")]
        //public IActionResult Authorize()
        //{
        //    var authorizationUri = $"{AuthorizationUrl}?response_type=code&client_id={ClientId}&redirect_uri={RedirectUri}&scope={Scope}&code_challenge={CodeChallenge}&code_challenge_method=S256";
        //    return Redirect(authorizationUri);
        //}

        //[HttpGet("callback")]
        //public async Task<IActionResult> Callback(string code)
        //{
        //    var content = new FormUrlEncodedContent(new[]
        //    {
        //    new KeyValuePair<string, string>("grant_type", "authorization_code"),
        //    new KeyValuePair<string, string>("client_id", ClientId),
        //    new KeyValuePair<string, string>("client_secret", ClientSecret),
        //    new KeyValuePair<string, string>("code", code),
        //    new KeyValuePair<string, string>("redirect_uri", RedirectUri),
        //    new KeyValuePair<string, string>("code_verifier", CodeVerifier),
        //    });

        //    var response = await _httpClient.PostAsync(TokenUrl, content);
        //    var responseString = await response.Content.ReadAsStringAsync();

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return StatusCode((int)response.StatusCode, responseString);
        //    }

        //    return Ok(responseString);
        //}
    }
}