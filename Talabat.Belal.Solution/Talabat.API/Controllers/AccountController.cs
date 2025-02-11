using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos.Account;
using Talabat.API.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Service;

namespace Talabat.API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authServices;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager , IAuthService authServices)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _authServices = authServices;
        }

        // login End Point
        [HttpPost("login")] // POST: api/account/login
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            // 1. check if email is founded
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            var userDto = new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authServices.CreateTokenAsync(user , _userManager),
            };

            return Ok(userDto);
        }



        [HttpPost("register")] // post: api/account/register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok(new UserDTO()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token = await _authServices.CreateTokenAsync(user, _userManager),
            });

        }
           
    }
}
