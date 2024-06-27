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

        public AccountService(IAccountRepo accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        
        
        public  async Task<ResponseDto<string>> RegisterUserAsync(SignUpDto signUp,string role)
        {
            var result  = new  ResponseDto<string>();
            try 
            {
                var user = await _accountRepo.FindUserByEmailAsync(signUp.Email);
                if (user!=null)
                 {
                      result.Message = Response.ACCOUNT_ALREADY_EXISTS.ResponseMessage;
                      result.StatusCode= Response.ACCOUNT_ALREADY_EXISTS.ResponseCode;
                      return result;
                }
                var existRole = await _accountRepo.RoleExist(role);
                 if(existRole == null)
                 {
                       result.Message = Response.ROLE_ALREADY_EXIST.ResponseMessage;
                       result.StatusCode = Response.ROLE_ALREADY_EXIST.ResponseCode;
                       return result;

                 }
                 //check for roles
                  var mapUser = _mapper.Map<ApplicationUser>(signUp);

                 var createUser = await _accountRepo.SignUpAsync(mapUser, signUp.Password);

                 if(createUser == null)
                 {
                     result.Message = Response.FAILED.ResponseMessage;
                     result.StatusCode = Response.FAILED.ResponseCode;
                     return result;
                 }
               
                var addRole  = await _accountRepo.AddRoleAsync( createUser, role);
                if (addRole == null)
                {
                     result.Message = Response.FAILED.ResponseMessage;
                     result.StatusCode = Response.FAILED.ResponseCode;
                     return result;

                }
                     
                 
                 //To do check if the role exist

                  result.Message= Response.SUCCESS.ResponseMessage;
                  result.StatusCode = Response.SUCCESS.ResponseCode;
                  result.Result =  " user is successfully created";

                }
            catch(Exception ex)
            { 
               result.Message= ex.Message;
               
                

            }
             return result;
          
        }

        
    }
}