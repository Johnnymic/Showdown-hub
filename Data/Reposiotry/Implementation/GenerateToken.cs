using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Showdown_hub.Data.Reposiotry.Interface;
using Showdown_hub.Models;

namespace Showdown_hub.Data.Reposiotry.Implementation
{
    public class GenerateToken : IGenerateJwt

    {

        // public Task<string> GenerateToken(ApplicationUser applicationUser)
        // {
        //     return Task.FromResult("");


        // }
         private  readonly UserManager<ApplicationUser> _userManager;

         private readonly IConfiguration _configuration;


        public GenerateToken(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            
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

        
    }
}