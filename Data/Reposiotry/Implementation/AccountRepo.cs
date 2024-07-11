using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Showdown_hub.Data.DbContext;
using Showdown_hub.Data.Reposiotry.Interface;
using Showdown_hub.Models;
using Showdown_hub.Models.Dtos;

namespace Showdown_hub.Data.Reposiotry.Implementation
{
    public class AccountRepo : IAccountRepo
    {

         private readonly UserManager<ApplicationUser> _userManager;

         private readonly EventHubContext _context;

          private readonly RoleManager<IdentityRole> _roleManager;

          private  readonly IConfiguration _configuration;


          public AccountRepo(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager , EventHubContext context, IConfiguration configuration)
          {
              _userManager = userManager;
              _roleManager = roleManager;
              _context = context;
              _configuration =configuration;
          }

        public  async Task<bool> AddRoleAsync(ApplicationUser applicationUser, string role)
        {
              var AddRole =  await _userManager.AddToRoleAsync( applicationUser, role);
             return  AddRole.Succeeded ? true : false;
              
        }

        public async Task<bool> CheckAccountPassword(ApplicationUser user, string password)
        {
            
             var passwordCheck = await _userManager.CheckPasswordAsync(user, password);

             return passwordCheck ? true : false;
        }

        
        

        public async Task<bool> ConfirmEmail(ApplicationUser applicationUser, string token)
        {
             
             var email = await _userManager.ConfirmEmailAsync(applicationUser, token);
             if(email.Succeeded)
             {
                return true;
             }
             return false;
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
                 return user != null ? user : null ;
        }

        public  async Task<ApplicationUser> FindUserById(string id)
        {
             var user = await _userManager.FindByIdAsync(id);
             return user != null ? user : null ;
        }

        public async Task<string> ForgetPassword(ApplicationUser applicationUser)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
            if(token != null){
                return token;
            }

            return  "Invalid passowrd";
        }

      
            

        public Task<IList<string>> GetUserRoles(ApplicationUser applicationUser)
        {
            var userRole = _userManager.GetRolesAsync(applicationUser);

            return userRole;
        }

      

        public Task<bool> RemoveRoleAysnc(ApplicationUser applicationUser, IList<string> roles)
        {
            throw new NotImplementedException();
        }

        public  async Task<bool> ResetPasswordAsyn(ApplicationUser applicationUser, ResetPassword resetPassword)
        {
             var  result =  await _userManager.ResetPasswordAsync(applicationUser, resetPassword.Token, resetPassword.Password);

            return result.Succeeded ? true : false;
        }

        public async Task<bool> RoleExist(string roleName)
        {
             var role = await _roleManager.RoleExistsAsync(roleName);

             return role ? role : false ;
        }

        public  async Task<ApplicationUser> SignUpAsync(ApplicationUser user, string password)
        {
             user.profileStatus = Models.Enums.ProfileStatus.New;
             var result = await _userManager.CreateAsync(user, password);
              return result.Succeeded ? user : null;
        }

        public Task<bool> UpdatedLoginStatus(ApplicationUser applicationUser)
        {
            throw new Exception();
        }

        public   async Task<string> GenerateJwtToken(ApplicationUser applicationUser)
        {
            var roles =  await _userManager.GetRolesAsync(applicationUser);
             
             var name = applicationUser.FirstName + " " + applicationUser.LastName;

             var authClaims = new  List<Claim>()
             {
                new Claim(ClaimTypes.Name, name),
                new Claim(JwtRegisteredClaimNames.Jti , applicationUser.Id),
                new Claim(ClaimTypes.Email, applicationUser.Email),
                new Claim(ClaimTypes.UserData, applicationUser.Id),
    
             };

             foreach(var role in roles)
             {
                   authClaims.Add(new Claim(ClaimTypes.Role, role));
             }
             var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

             var Token = new JwtSecurityToken
             (
                issuer: _configuration["JWT:ValidIssuer"],
                audience : _configuration["ValidAudience"],
                expires : DateTime.Now.AddDays(1),
                claims : authClaims,
                signingCredentials : new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha384Signature)
             );


           var JwtToken = new JwtSecurityTokenHandler().WriteToken(Token);
           return JwtToken;
        }

        public Task<IList<string>> GetUserRoles()
        {
            return null;
        }

        public async Task<string> CreateNewRole(string newRole)
        {
            
            var createRole = await _roleManager.CreateAsync( new IdentityRole(newRole));
            return createRole.ToString() ?? string.Empty;
        }

        public  async Task<bool> LogoutUser(ApplicationUser applicationUser)
        {
             applicationUser.LockoutEnabled= true;
             
              var logout = await _userManager.GetLockoutEnabledAsync(applicationUser);

              return logout;
        }

        public async Task<bool> UpdateUser(ApplicationUser applicationUser)
        {
            var user = await _userManager.UpdateAsync(applicationUser);
            
            return user.Succeeded ? true: false;
        }

    }
}