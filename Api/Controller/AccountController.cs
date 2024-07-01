using Microsoft.AspNetCore.Mvc;
using Showdown_hub.Api.Services.Interface;
using Showdown_hub.Models.Dtos;

namespace Showdown_hub.Api
{
     [Route("api/user")]
     [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser( SignUpDto signUp)
        {
            var registerUser = await _accountService.RegisterUserAsync(signUp, "USER");
            if (registerUser.StatusCode == "00")
            {
                return  Ok(registerUser);
            }
            else if( registerUser.StatusCode == "99")
            {
                return NotFound(registerUser);

            }
            else
            {
                return BadRequest(registerUser);
            }
            

        }

        [HttpPost("/login")]
        public async Task<IActionResult> LoginUser(LoginDto login)
        {
            var loginUser = await _accountService.LoginUser(login);
            if (loginUser.StatusCode == "00")
            {
                      return Ok(login);
            }
            else if (loginUser.StatusCode == "99")
            {
                return NotFound(LoginUser);
            }
            else
            {
                  return BadRequest(LoginUser);
            }

        }

        [HttpPost("/role")]
        public async Task<IActionResult> CreateRoles(CreateRoleDto createRole)
        {
             var role = await _accountService.CreateRoleAsync(createRole);
             
             return Ok(role); 

        }

        

    }
}