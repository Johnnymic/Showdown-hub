using Showdown_hub.Models;
using Showdown_hub.Models.Dtos;

namespace Showdown_hub.Api.Services.Interface
{
    public interface IAccountService
    {
        Task<ResponseDto<string>> RegisterUserAsync(SignUpDto signUp);
    }
}