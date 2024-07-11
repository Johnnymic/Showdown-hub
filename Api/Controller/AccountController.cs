using Microsoft.AspNetCore.Mvc;
using Showdown_hub.Api.Services.Interface;
using Showdown_hub.Models;
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

        [HttpPost("/register")]
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
                      return Ok(loginUser);
            }
            else if (loginUser.StatusCode == "99")
            {
                return NotFound(loginUser);
            }
            else
            {
                  return BadRequest(loginUser);
            }

        }

        [HttpPost("/role")]
        public async Task<IActionResult> CreateRoles(CreateRoleDto createRole)
        {
             var role = await _accountService.CreateRoleAsync(createRole);

             if (role.StatusCode == "00")
             {
                return Ok(role);
             }
             else if(role.StatusCode=="99")
             {
                return NotFound(role);
             }
             else
             {
                return BadRequest(role);
             }

         }
      
       [HttpPost("/forget/password")]
         public async Task<IActionResult> ForgetPassword(string email){
        //forget and reset password
        var forgetPassword = await  _accountService.ForgetPasswordAsync(email);
         
         if(forgetPassword.StatusCode == "00")
         {
             return Ok(forgetPassword);

         }
         else if(forgetPassword.StatusCode== "99")
         {
             return NotFound(forgetPassword);

         }
         else
         {
             return BadRequest(forgetPassword);
         }
         }

         [HttpPost("/reset/password")]
         public async Task<IActionResult> resetPassword(ResetPassword resetPassword) 
         {
            var resetPass = await _accountService.ResetPasswordAsync( resetPassword);

            if (resetPass.StatusCode == "00")
            {
                return Ok(resetPassword);
            }
            else
            {
                return BadRequest(resetPass);
            }
         }

         [HttpPost("/logout")]
         public async Task<IActionResult> LogoutUser(string email)
         {
            var user = await _accountService.LogOutUser(email);
            if (user.StatusCode == "00")
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(user);
            }

         }

        
           
       
    }

        

}
