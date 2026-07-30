using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Identity;

namespace ECommerce.Application.Contacts
{
    public interface IAuthenticationService
    {
        // Login
        // Email + Password => Token, Email, DisplayName
        Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);
    }
}
