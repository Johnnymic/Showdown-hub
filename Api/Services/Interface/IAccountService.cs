using Showdown_hub.Models;
using Showdown_hub.Models.Dtos;

namespace Showdown_hub.Api.Services.Interface
{
    public interface IAccountService
    {
        Task<ResponseDto<string>> CreateRoleAsync(CreateRoleDto createRole);
        Task<ResponseDto<string>> ForgetPasswordAsync(string email);
        Task <ResponseDto<string>> LoginUser(LoginDto login);
        Task<ResponseDto<string>> RegisterUserAsync(SignUpDto signUp,string role);
        
        Task<ResponseDto<string>> ResetPasswordAsync(ResetPassword resetPassword);

        Task<ResponseDto<string>> LogOutUser(string UserEmail);

         Task<ResponseDto<string>> VerifyEmail(string Email);

         Task<ResponseDto<string>> UpdateUser(UpdateUserDto updateUser, string email);
        
        
    }
}