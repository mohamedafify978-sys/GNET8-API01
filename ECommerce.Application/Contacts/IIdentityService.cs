using ECommerce.Application.Common;

namespace ECommerce.Application.Contacts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken token = default);
        Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken token = default);
    }
}
