using ECommerce.Application.Common;
using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Identity;
using ECommerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken token = default)
        {
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }

            return Result<IdentityUserResult>.Ok(new IdentityUserResult(
                user.Id,
                user.Email!,
                user.DisplayName,
                user.UserName!
                ));
        }

        public async Task<Result<IReadOnlyList<string>>> GetRolesAsync(string email, CancellationToken token = default)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                return Result<IReadOnlyList<string>>.Fail(Error.NotFound($"User '{email}' Is Not Found"));

            var roles = await userManager.GetRolesAsync(user);
            return Result<IReadOnlyList<string>>.Ok(roles.ToList());
        }

        public async Task<Result<AddressDto>> GetAddressByEmailAsync(string email, CancellationToken token = default)
        {
            var user = await userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email, token);

            if (user == null)
                return Result<AddressDto>.Fail(Error.NotFound($"User '{email}' Is Not Found"));

            if (user.Address == null)
                return Result<AddressDto>.Fail(Error.NotFound("Address Not Found"));

            return new AddressDto
            {
                FirstName = user.Address.FirstName,
                LastName = user.Address.LastName,
                City = user.Address.City,
                Street = user.Address.Street,
                Country = user.Address.Country
            };
        }

        public async Task<Result<AddressDto>> UpSertAddressAsync(string email, AddressDto addressDto, CancellationToken token = default)
        {
            var user = await userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email, token);

            if (user == null)
                return Result<AddressDto>.Fail(Error.NotFound($"User '{email}' Is Not Found"));

            if (user.Address is null)
            {
                user.Address = new Address
                {
                    FirstName = addressDto.FirstName,
                    LastName = addressDto.LastName,
                    City = addressDto.City,
                    Street = addressDto.Street,
                    Country = addressDto.Country
                };
            }
            else
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.Street = addressDto.Street;
            }

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return Result<AddressDto>.Fail(Error.Failure("Failure", string.Join("; ", result.Errors.Select(e => e.Description))));

            return addressDto;
        }

        public async Task<Result<bool>> EmailExistsAsync(string email, CancellationToken token = default)
            => await userManager.FindByEmailAsync(email) is not null;
    }
}
