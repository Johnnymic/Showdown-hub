using Microsoft.AspNetCore.Identity;
using Showdown_hub.Models.Entities;
using Showdown_hub.Models.Enums;

namespace Showdown_hub.Models
{
    public class ApplicationUser : IdentityUser
    {

       public string FirstName {get; set;}

       public string LastName {get; set;}

       private string PhoneNumber {get; set;}

       public Gender Gender {get; set;}

       public EventType eventType {get; set;}

       public string Address {get; set;}

       public DateTime DateOfBirth{ get; set;}

       public bool IsEmailVerified {get; set;}

       public ICollection<Account> Account {get; set;}

       
    }
}