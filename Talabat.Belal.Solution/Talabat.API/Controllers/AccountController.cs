using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.Dtos;
using Talabat.API.Dtos.Account;
using Talabat.API.Errors;
using Talabat.API.Extensions;
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
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager ,
            IAuthService authServices,
            IMapper mapper
            
            )
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _authServices = authServices;
            this._mapper = mapper;
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

        [Authorize]
        [HttpGet("getCurrenUser")] // GET: /api/account/getCurrenUser
        public async Task<ActionResult<UserDTO>> getCuurentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            var  user = await _userManager.FindByEmailAsync(email);

            return Ok(
                new UserDTO()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await _authServices.CreateTokenAsync(user, _userManager)
                }
                );

        }


        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> address()
        {

            var user = await _userManager.FindUserAddressByEmailAsync(User);

            
            
            return Ok(_mapper.Map<Address , AddressDTO>(user.Address));
        }



    }
}
