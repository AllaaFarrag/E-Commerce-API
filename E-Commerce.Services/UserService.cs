using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Identity;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserService(ITokenService tokenService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            //Email => User => Password => Create Token => Dto

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if(user is not null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user , dto.Password , false);

                if (result.Succeeded)
                    return new UserDto
                    {
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        Token = _tokenService.GenerateToken(user)
                    };
            }
            return null;
        }

        public async Task<UserDto> RegisterAsync(RegesterDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is not null) throw new Exception("Email Exists");

            var appUser = new ApplicationUser
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                UserName = dto.DisplayName,
            }; 

            var result = await _userManager.CreateAsync(appUser, dto.Password);
            if (!result.Succeeded) throw new Exception("Error");

            return new UserDto
            {
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                Token = _tokenService.GenerateToken(appUser)
            };
            //throw new NotImplementedException();
        }
    }
}
