using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
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

       private readonly  GenerateToken  _generateToken;

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

         public async Task<ResponseDto<string>> LoginUser(LoginDto login)
        {// check if the email exist.
             var result = new ResponseDto<string>(); 
          
          try
          {

          
              var currentUser = await _accountRepo.FindUserByEmailAsync(login.Email);
               if (currentUser == null)
               {
                      result.Message = Response.INVALID_EMAIL.ResponseMessage;
                      result.StatusCode = Response.INVALID_EMAIL.ResponseCode;
                      return result;
               }
               var checkPassword = await _accountRepo.CheckAccountPassword(currentUser,login.Password);

               if (checkPassword == null)
               {
                    result.Message = Response.INVALID_ACCOUNT.ResponseMessage;
                    result.StatusCode= Response.INVALID_ACCOUNT.ResponseCode;
                    return result;
               }

              var token = await _accountRepo.GenerateJwtToken(currentUser);
                if(token == null)
                {
                    result.Message = Response.INVALID_TOKEN.ResponseMessage;
                    result.StatusCode = Response.INVALID_TOKEN.ResponseCode;
                    return result;
                }

                result.Message = Response.SUCCESS.ResponseMessage;
                result.StatusCode =Response.SUCCESS.ResponseCode;
                result.ErrorMessage = new List<string>{"No error, user have successfully sign in"};
                


          }
          catch(Exception ex)
          {
              result.Message = ex.Message;

          }

          return result;
        
        }

        public  async Task<ResponseDto<string>> CreateRoleAsync(CreateRoleDto createRole)
        {
            //check if the role exist if it does thrown an expexistion o exist create a new role 
            var result = new ResponseDto<string>();
     try
     {

     
             var rolesExist = await _accountRepo.RoleExist(createRole.RoleName);

             if(rolesExist != null)
             {
                 result.Message = Response.ROLE_ALREADY_EXIST.ResponseMessage;
                 result.StatusCode =Response.ACCOUNT_ALREADY_EXISTS.ResponseCode;
             }

             // create a role 
              var addRole =  await _accountRepo.CreateNewRole(createRole.RoleName);

              if(addRole == null)
              {
                result.Message = Response.FAILED.ResponseMessage;
                result.StatusCode=Response.FAILED.ResponseCode;
                return result;
              }

              result.Message= Response.SUCCESS.ResponseMessage;
              result.StatusCode = Response.SUCCESS.ResponseCode;
              result.Result = "ROLE SUCCESSFULL ADDED";
     }
     catch(Exception ex)
     {
        result.Message = ex.Message;
     }      

             return  result;
        }
    }
}