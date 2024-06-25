using Microsoft.AspNetCore.Identity;
using Showdown_hub.Models.Entities;
using Showdown_hub.Models.Enums;

namespace Showdown_hub.Models.Dtos
{
    public class SignUpDto 
    {
        
        public  string Email { get; set; }

        public  string Password { get; set; }

       public string FirstName {get; set;}

       public string LastName {get; set;}

       public string PhoneNumber {get; set;}
       

       public string Address {get; set;}

       

       public bool IsEmailVerified {get; set;}

      

        
    }
}