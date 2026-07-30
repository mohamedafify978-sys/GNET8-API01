using ECommerce.Application.Common;
using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Identity;

namespace ECommerce.Application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService identityService;

        public AuthenticationService(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            // Get user by email
            var userResult = await identityService.FindUserByEmailAsync(loginDto.Email, ct);
            if (!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);

            // Check password
            var passwordResult = await identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password, ct);
            if (!passwordResult.IsSuccess)
                return Result<UserDto>.Fail(passwordResult.Errors);
            if (!passwordResult.data)
                return Result<UserDto>.Fail(Error.Unauthorized("Invalid Email Or Password"));

            return new UserDto()
            {
                Email = loginDto.Email,
                DisplayName = userResult.data!.DisplayName,
                Token = "Token" // TODO: replace with real JWT generation later
            };
        }
    }
}
