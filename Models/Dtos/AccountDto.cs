using System.ComponentModel.DataAnnotations;
using Showdown_hub.Models.Enums;

namespace Showdown_hub.Models.Dtos
{
    public class AccountDto
    {
        [Required(ErrorMessage = "Gender is required.")]
       public int Gender {get; set;}

      public string  DateOfBirth{ get; set;}

     [Required(ErrorMessage = "Address is required")]
      public string Address {get; set;}

     [Required(ErrorMessage = "AccountName is required.")]
      [StringLength(50, ErrorMessage = "AccountName cannot be longer than 50 characters.")]
     public string AccountName { get; set; }

     [Required(ErrorMessage = "BankName is required.")]
      [StringLength(12, ErrorMessage = "AccountName cannot be longer than 50 characters.")]
     public string BankName { get; set; }

      public string BankType { get; set;}

       
     [Required(ErrorMessage = "AccountNumber is required.")]
      [StringLength(12, ErrorMessage = "AccountNumber cannot be longer than 50 characters.")]
     public string AccountNumber { get; set; }



   


        
    }

}