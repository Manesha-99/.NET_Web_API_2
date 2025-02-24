using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //Create Function to Register User-----------------------------------------
        //Post  /api/auth/Register
        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,

            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {

                if ((registerRequestDto.Roles != null) && (registerRequestDto.Roles.Any()))
                {

                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {

                        return Ok("User registration success.");
                    }
                }
            }

            return BadRequest();
        }



        //Create Function to login-------------------------------------------------

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto) 
        {
            var user = await userManager.FindByNameAsync(loginRequestDto.Username);

            if (user != null) {
                
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {

                    //Get Roles
                    var roles = await userManager.GetRolesAsync(user);

                        if (roles != null)
                        {
                            //Create Token

                            var jwtToken =  tokenRepository.CreateJWTToken(user, roles.ToList());

                            var response = new LoginResponseDto
                            {
                                JwtToken = jwtToken,
                            };    

                             return Ok(response);

                        }
                }

                return BadRequest("Credentials mismatch..");
            }

            return BadRequest("Invalid Username..");

        }
    }

}
