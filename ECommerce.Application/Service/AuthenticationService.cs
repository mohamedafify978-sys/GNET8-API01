using ECommerce.Application.Common;
using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Identity;

namespace ECommerce.Application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService identityService;
        private readonly ITokenService tokenService;

        public AuthenticationService(IIdentityService identityService, ITokenService tokenService)
        {
            this.identityService = identityService;
            this.tokenService = tokenService;
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

            var rolesResult = await identityService.GetRolesAsync(loginDto.Email, ct);
            if (!rolesResult.IsSuccess)
                return Result<UserDto>.Fail(rolesResult.Errors);

            var user = userResult.data!;
            var token = tokenService.CreateToken(user.Id, user.Email, user.UserName, rolesResult.data!);

            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token
            };
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var result = await identityService.CreateUserAsync(registerDto, ct);
            if (!result.IsSuccess || result.data is null)
                return Result<UserDto>.Fail(result.Errors);

            return new UserDto()
            {
                Email = result.data.Email,
                DisplayName = result.data.DisplayName,
                Token = tokenService.CreateToken(result.data.Id, result.data.Email, result.data.UserName, [])
            };
        }

        public async Task<Result<bool>> CheckEmailAsync(string email, CancellationToken ct = default)
            => await identityService.EmailExistsAsync(email, ct);

        public async Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct = default)
        {
            var userResult = await identityService.FindUserByEmailAsync(email, ct);
            if (!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);

            var rolesResult = await identityService.GetRolesAsync(email, ct);
            if (!rolesResult.IsSuccess)
                return Result<UserDto>.Fail(rolesResult.Errors);

            var user = userResult.data!;
            var token = tokenService.CreateToken(user.Id, user.Email, user.UserName, rolesResult.data!);

            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token
            };
        }

        public async Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default)
            => await identityService.GetAddressByEmailAsync(email, ct);

        public async Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string email, CancellationToken ct = default)
            => await identityService.UpSertAddressAsync(email, addressDto, ct);
    }
}
