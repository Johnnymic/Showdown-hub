using AutoMapper;
using Showdown_hub.Api.Services.Interface;
using Showdown_hub.Data.Reposiotry.Implementation;
using Showdown_hub.Data.Reposiotry.Interface;
using Showdown_hub.Models;
using Showdown_hub.Models.Dtos;

namespace Showdown_hub.Api.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;

        private readonly IMapper _mapper;

        public AccountService(AccountRepo accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        
        
        public  async Task<ResponseDto<string>> RegisterUserAsync(SignUpDto signUp)
        {
            var result  = new  ResponseDto<string>();
            try 
            {
                var user = await _accountRepo.FindUserByEmailAsync(signUp.email);
                if (user!=null)
                 {
                      result.Message = Response.ACCOUNT_ALREADY_EXISTS.ResponseMessage;
                      result.StatusCode= Response.ACCOUNT_ALREADY_EXISTS.ResponseCode;
                      return result;
                }
                 //check for roles
                 var mapUser = _mapper.Map<ApplicationUser>(signUp);

                 var createUser = await _accountRepo.SignUpAsync(mapUser, signUp.password);

                 if(createUser == null)
                 {
                     result.Message = Response.FAILED.ResponseMessage;
                     result.StatusCode = Response.FAILED.ResponseCode;
                     return result;
                 }
                 //To do check if the role exist

                  result.Message= Response.SUCCESS.ResponseMessage;
                  result.StatusCode = Response.SUCCESS.ResponseCode;
                  result.Result =  $"{createUser.Email} is successfully created";

                }
            catch(Exception ex)
            { 
               result.Message= ex.Message;
                

            }
             return result;
          
        }

        
    }
}