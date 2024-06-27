using Microsoft.AspNetCore.Identity;
using Showdown_hub.Data.DbContext;
using Showdown_hub.Data.Reposiotry.Interface;
using Showdown_hub.Models;

namespace Showdown_hub.Data.Reposiotry.Implementation
{
    public class AccountRepo : IAccountRepo
    {

         private readonly UserManager<ApplicationUser> _userManager;

         private readonly EventHubContext _context;

          private readonly RoleManager<IdentityRole> _roleManager;


          public AccountRepo(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager , EventHubContext context)
          {
              _userManager = userManager;
              _roleManager = roleManager;
              _context = context;
          }

        public  async Task<bool> AddRoleAsync(ApplicationUser applicationUser, string role)
        {
              var AddRole =  await _userManager.AddToRoleAsync( applicationUser, role);
             return  AddRole.Succeeded ? true : false;
              
        }

        public Task<bool> CheckAccountPassword(ApplicationUser user, string password)
        {
            throw new NotImplementedException();
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

        public Task<IList<string>> GetUserRoles()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveRoleAysnc(ApplicationUser applicationUser, IList<string> roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsyn(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RoleExist(string roleName)
        {
             var role = await _roleManager.RoleExistsAsync(roleName);

             return role ? role : false ;
        }

        public  async Task<ApplicationUser> SignUpAsync(ApplicationUser user, string password)
        {
              var result = await _userManager.CreateAsync(user, password);
              return result.Succeeded ? user : null;
        }

        public Task<bool> UpdatedLoginStatus(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }
    }
}