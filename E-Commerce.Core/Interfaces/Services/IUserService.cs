using E_Commerce.Core.DataTransferObjects;

namespace E_Commerce.Core.Interfaces.Services
{
    public interface IUserService
    {
        public Task<UserDto?> LoginAsync(LoginDto dto);
        public Task<UserDto> RegisterAsync(RegesterDto dto);
    }
}
