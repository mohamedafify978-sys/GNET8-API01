using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Identity;

namespace ECommerce.Application.Contacts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken token = default);
        Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken token = default);
        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken token = default);
        Task<Result<IReadOnlyList<string>>> GetRolesAsync(string email, CancellationToken token = default);
        Task<Result<AddressDto>> GetAddressByEmailAsync(string email, CancellationToken token = default);
        Task<Result<AddressDto>> UpSertAddressAsync(string email, AddressDto addressDto, CancellationToken token = default);
        Task<Result<bool>> EmailExistsAsync(string email, CancellationToken token = default);
    }
}
