using ECommerce.Application.Common;
using ECommerce.Application.Contacts;
using ECommerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Identity.Services
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken token = default)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<bool>.Fail(Error.NotFound("User Is Not Found", $"User With Email {email} Is Not Found"));
            else
                return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken token = default)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<IdentityUserResult>.Fail(Error.NotFound("User Is Not Found", $"User With Email {email} Is Not Found"));
            else
                return Result<IdentityUserResult>.Ok(new IdentityUserResult(
                    user.Id,
                    user.Email!,
                    user.DisplayName,
                    user.UserName!
                    ));
        }
    }
}
