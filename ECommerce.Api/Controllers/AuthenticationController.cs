using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
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

        // Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
            => ToActionResult(await authenticationService.RegisterAsync(registerDto));

        // Check if an email is already used (e.g. while filling the register form)
        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email)
            => ToActionResult(await authenticationService.CheckEmailAsync(email));

        // Needs a valid JWT in the Authorization header: Bearer <token>
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
            => ToActionResult(await authenticationService.GetCurrentUserAsync(GetEmailFromToken()));

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
            => ToActionResult(await authenticationService.GetUserAddressAsync(GetEmailFromToken()));

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
            => ToActionResult(await authenticationService.UpdateUserAddressAsync(addressDto, GetEmailFromToken()));
    }
}
