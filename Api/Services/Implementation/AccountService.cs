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
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepo accountRepo, IMapper mapper, ILogger<AccountService> logger)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseDto<string>> RegisterUserAsync(SignUpDto signUp, string role)
        {
            var result = new ResponseDto<string>();
            try 
            {
                var user = await _accountRepo.FindUserByEmailAsync(signUp.Email);
                if (user != null)
                {
                    result.Message = Response.ACCOUNT_ALREADY_EXISTS.ResponseMessage;
                    result.StatusCode = Response.ACCOUNT_ALREADY_EXISTS.ResponseCode;

                    _logger.LogWarning("Registration failed: Email {Email} already exists.", signUp.Email);
                    return result;
                }

                _logger.LogInformation("Starting user registration for email {Email}.", signUp.Email);

                var existRole = await _accountRepo.RoleExist(role);
                if (existRole == null)
                {
                    result.Message = Response.ROLE_ALREADY_EXIST.ResponseMessage;
                    result.StatusCode = Response.ROLE_ALREADY_EXIST.ResponseCode;

                    _logger.LogWarning("Registration failed: Role {Role} does not exist.", role);
                    return result;
                }

                var mapUser = _mapper.Map<ApplicationUser>(signUp);
                var createUser = await _accountRepo.SignUpAsync(mapUser, signUp.Password);

                if (createUser == null)
                {
                    result.Message = Response.FAILED.ResponseMessage;
                    result.StatusCode = Response.FAILED.ResponseCode;

                    _logger.LogError("User creation failed for email {Email}.", signUp.Email);
                    return result;
                }

                _logger.LogInformation("User {UserId} created successfully.", createUser.Id);

                var addRole = await _accountRepo.AddRoleAsync(createUser, role);
                if (addRole == null)
                {
                    result.Message = Response.FAILED.ResponseMessage;
                    result.StatusCode = Response.FAILED.ResponseCode;

                    _logger.LogError("Role {Role} assignment failed for user {UserId}.", role, createUser.Id);
                    return result;
                }

                _logger.LogInformation("Role {Role} assigned to user {UserId} successfully.", role, createUser.Id);

                result.Message = Response.SUCCESS.ResponseMessage;
                result.StatusCode = Response.SUCCESS.ResponseCode;
                result.Result = "User successfully created";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                _logger.LogError(ex, "An error occurred during registration for email {Email}.", signUp.Email);
            }

            return result;
        }

        public async Task<ResponseDto<string>> LoginUser(LoginDto login)
        {
            var result = new ResponseDto<string>(); 
            try
            {
                var currentUser = await _accountRepo.FindUserByEmailAsync(login.Email);
                if (currentUser == null)
                {
                    result.Message = Response.INVALID_EMAIL.ResponseMessage;
                    result.StatusCode = Response.INVALID_EMAIL.ResponseCode;

                    _logger.LogWarning("Login attempt failed: Invalid email {Email}.", login.Email);
                    return result;
                }

                _logger.LogInformation("User found for email {Email}.", login.Email);

                var checkPassword = await _accountRepo.CheckAccountPassword(currentUser, login.Password);
                if (checkPassword == null)
                {
                    result.Message = Response.INVALID_ACCOUNT.ResponseMessage;
                    result.StatusCode = Response.INVALID_ACCOUNT.ResponseCode;

                    _logger.LogWarning("Login attempt failed: Invalid password for user {UserId}.", currentUser.Id);
                    return result;
                }

                _logger.LogInformation("Password verification successful for user {UserId}.", currentUser.Id);

                var token = await _accountRepo.GenerateJwtToken(currentUser);
                if (token == null)
                {
                    result.Message = Response.INVALID_TOKEN.ResponseMessage;
                    result.StatusCode = Response.INVALID_TOKEN.ResponseCode;

                    _logger.LogError("Token generation failed for user {UserId}.", currentUser.Id);
                    return result;
                }

                _logger.LogInformation("Token generated successfully for user {UserId}.", currentUser.Id);

                result.Message = Response.SUCCESS.ResponseMessage;
                result.StatusCode = Response.SUCCESS.ResponseCode;
                result.ErrorMessage = new List<string> { "No error, user has successfully signed in" };
                result.Result = token;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                _logger.LogError(ex, "An error occurred during login for email {Email}.", login.Email);
            }

            return result;
        }

        public async Task<ResponseDto<string>> CreateRoleAsync(CreateRoleDto createRole)
        {
            var result = new ResponseDto<string>();
            try
            {
                var rolesExist = await _accountRepo.RoleExist(createRole.RoleName);
                if (rolesExist != null)
                {
                    result.Message = Response.ROLE_ALREADY_EXIST.ResponseMessage;
                    result.StatusCode = Response.ACCOUNT_ALREADY_EXISTS.ResponseCode;

                    _logger.LogWarning("Role creation failed: Role {RoleName} already exists.", createRole.RoleName);
                    return result;
                }

                _logger.LogInformation("Starting role creation for role {RoleName}.", createRole.RoleName);

                var addRole = await _accountRepo.CreateNewRole(createRole.RoleName);
                if (addRole == null)
                {
                    result.Message = Response.FAILED.ResponseMessage;
                    result.StatusCode = Response.FAILED.ResponseCode;

                    _logger.LogError("Role creation failed for role {RoleName}.", createRole.RoleName);
                    return result;
                }

                _logger.LogInformation("Role {RoleName} created successfully.", createRole.RoleName);

                result.Message = Response.SUCCESS.ResponseMessage;
                result.StatusCode = Response.SUCCESS.ResponseCode;
                result.Result = "Role successfully added";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                _logger.LogError(ex, "An error occurred during role creation for role {RoleName}.", createRole.RoleName);
            }

            return result;
        }

        public async Task<ResponseDto<string>> ForgetPasswordAsync(string email)
        {
            var result = new ResponseDto<string>();
            try
            {   
                 var currentUser = await _accountRepo.FindUserByEmailAsync(email);
                if (currentUser == null)
                {
                     result.Message = Response.INVALID_EMAIL.ResponseMessage;
                     result.StatusCode = Response.INVALID_EMAIL.ResponseCode;

                      _logger.LogWarning("Login attempt failed: Invalid email {Email}.", email);
                    return result;
                }

                   var  forgetPassword = await _accountRepo.ForgetPassword(currentUser);
                if(forgetPassword == null)
                {
                    result.Message = Response.FAILED.ResponseMessage;
                    result.StatusCode = Response.FAILED.ResponseCode;

                    _logger.LogError("User is changing their password {forgetPassword}.", forgetPassword);
                }   
                  

                result.Message = Response.SUCCESS.ResponseMessage;
                result.StatusCode = Response.SUCCESS.ResponseCode;
                result.Result = "User has successfully  change their password"; 


            }
            catch (Exception ex)
            {
                 result.Message = ex.Message;
                _logger.LogError(ex, "An error occurred during forget  password for  {Email}.", email);

            }

            return result;
        }

        public async Task<ResponseDto<string>> ResetPasswordAsync(ResetPassword resetPassword)
        {
              var result = new ResponseDto<string>();   

            try
              {
                 var userExist = await _accountRepo.FindUserByEmailAsync(resetPassword.Email);
                 if(userExist == null)
                 {
                    result.Message = Response.INVALID_ACCOUNT.ResponseMessage;
                    result.StatusCode = Response.INVALID_ACCOUNT.ResponseCode;

                    _logger.LogWarning("Registration failed: Email {Email} already exists.", resetPassword.Email);
                    return result;
                 }
                 var response = await _accountRepo.ResetPasswordAsyn(userExist, resetPassword);
                 if(response == null)
                 {
                    result.Message = Response.FAILED.ResponseMessage;
                    result.StatusCode = Response.FAILED.ResponseCode;
                      _logger.LogError("User is reset their  their password {forgetPassword}.", resetPassword.ConfirmPassword);
                 }

                result.Message = Response.SUCCESS.ResponseMessage;
                result.StatusCode = Response.SUCCESS.ResponseCode;
                result.Result = "User has successfully  reset their password"; 


              }catch(Exception ex)
              {
                result.Message=ex.Message;
                result.StatusCode = Response.FAILED.ResponseCode;
              }

            return result;
        }

        public Task<ResponseDto<string>> LogOutUser(string UserEmail)
        {
            throw new NotImplementedException();
        }
    }

}
