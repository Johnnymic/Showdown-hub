using Showdown_hub.Models;
using Showdown_hub.Models.Dtos;

namespace Showdown_hub.Api.Services.Interface
{
    public interface IAccountService
    {
        Task<ResponseDto<string>> CreateRoleAsync(CreateRoleDto createRole);
        Task <ResponseDto<string>> LoginUser(LoginDto login);
        Task<ResponseDto<string>> RegisterUserAsync(SignUpDto signUp,string role);

        
    }
}