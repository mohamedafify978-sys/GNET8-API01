using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = default!;
        public Address? Address { get; set; }
    }
}
