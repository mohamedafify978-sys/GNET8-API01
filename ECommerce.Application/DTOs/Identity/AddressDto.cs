using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.Identity
{
    public class AddressDto
    {
        [Required]
        public string FirstName { get; set; } = default!;
        [Required]
        public string LastName { get; set; } = default!;
        [Required]
        public string City { get; set; } = default!;
        [Required]
        public string Street { get; set; } = default!;
        [Required]
        public string Country { get; set; } = default!;
    }
}
