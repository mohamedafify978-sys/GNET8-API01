using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        // Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
            => ToActionResult(await authenticationService.LoginAsync(loginDto));
    }
}
