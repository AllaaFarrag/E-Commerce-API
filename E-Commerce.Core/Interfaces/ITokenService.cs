using E_Commerce.Core.Entities.Identity;

namespace E_Commerce.Core.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(ApplicationUser user);
    }
}
