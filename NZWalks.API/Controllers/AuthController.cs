using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Diagnostics.Contracts;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Register User
        // Path : localhost:portNumber/api/Auth/Register

        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository; 
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // Assign Roles to User
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User Registered Successfully.You may login");
                    }
                }
            }

            return BadRequest(identityResult.Errors);

        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var isPasswordValid = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (isPasswordValid)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    //Create JWT Token
                    var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());

                    var response = new LoginResponseDto
                    {
                        JwtToken = jwtToken
                    };
                    return Ok(response);
                }
            }
            return Unauthorized("Invalid Username or Password");
        }
    }
}