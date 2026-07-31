using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.Identity
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required, MinLength(8)]
        public string Password { get; set; } = default!;

        [Required]
        public string UserName { get; set; } = default!;

        [Required]
        public string DisplayName { get; set; } = default!;

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
