using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Identity;

namespace ECommerce.Application.Contacts
{
    public interface IAuthenticationService
    {
        // Login
        // Email + Password => Token, Email, DisplayName
        Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);

        Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default);

        Task<Result<bool>> CheckEmailAsync(string email, CancellationToken ct = default);

        Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct = default);

        Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default);

        Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string email, CancellationToken ct = default);
    }
}
