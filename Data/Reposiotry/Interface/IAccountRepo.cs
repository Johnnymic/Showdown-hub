using Microsoft.AspNetCore.Identity;
using Showdown_hub.Models;
using Showdown_hub.Models.Dtos;

namespace Showdown_hub.Data.Reposiotry.Interface
{
    public interface IAccountRepo
    {
         Task<ApplicationUser> SignUpAsync(ApplicationUser user,string password);

         Task<bool> CheckAccountPassword(ApplicationUser user,string password);

         Task< bool>UpdatedLoginStatus(ApplicationUser applicationUser);

         Task<bool>ConfirmEmail(ApplicationUser applicationUser, string token);

         Task<bool> AddRoleAsync(ApplicationUser applicationUser,string role);

         Task<string> ForgetPassword(ApplicationUser applicationUser);

         Task<string> CreateNewRole(string newRole);

         Task<bool> RemoveRoleAysnc(ApplicationUser applicationUser ,IList<string> roles);

         Task<bool> ResetPasswordAsyn(ApplicationUser applicationUser,ResetPassword resetPassword);

         Task<bool>RoleExist(string roleName);

         Task<ApplicationUser> FindUserByEmailAsync(string email);

         Task<ApplicationUser> FindUserById(string id);

         Task<IList<string>> GetUserRoles();

         Task<string>GenerateJwtToken(ApplicationUser applicationUser); 

         Task<bool>LogoutUser(ApplicationUser applicationUser);

         Task<bool> UpdateUser(ApplicationUser applicationUser);

         Task<bool> DeleteUser(ApplicationUser appllicationUser);

    }
}