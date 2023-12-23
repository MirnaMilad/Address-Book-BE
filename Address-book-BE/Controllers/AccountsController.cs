using Address_Book.Core.Entities;
using Address_Book.Core.services;
using Address_book_BE.Dtos;
using Address_book_BE.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Address_book_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        //private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService
            //IMapper mapper

            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            //_mapper = mapper;
        }
        //Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var User = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest(new ApiResponse(400, "This email is Already exists please login"));
            }
            var Result = await _userManager.CreateAsync(User, model.Password);
            if (!Result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }
            var ReturnedUser = new UserDto()
            {
                Token = await _tokenService.CreateTokenAsync(User)
            };

            return Ok(ReturnedUser);
        }

        //Login

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiResponse(401));

            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

            if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));


            return Ok(new UserDto()
            {
                Token = await _tokenService.CreateTokenAsync(User)
            });

        }
    };
}
