using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.Identity
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
