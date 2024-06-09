using Microsoft.AspNetCore.Identity;

namespace Showdown_hub.Models
{
    public class ApplicationUser : IdentityUser
    {
       public string Email;
       public string Password;

       public string FirstName;

       public string LastName;

       public string PhoneNumber;

       public Gender gender
    }
}