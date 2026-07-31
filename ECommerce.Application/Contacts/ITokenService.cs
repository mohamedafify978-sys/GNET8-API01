namespace ECommerce.Application.Contacts
{
    public interface ITokenService
    {
        string CreateToken(string userId, string email, string userName, IEnumerable<string> roles);
    }
}
