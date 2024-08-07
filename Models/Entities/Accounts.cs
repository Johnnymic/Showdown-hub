using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Showdown_hub.Models.Entities
{
    public class Accounts
    {
        [Key]
        public string Id { get; set; } =Guid.NewGuid().ToString();

        [Required]
        public string AccountName { get; set; }
        [Required]
         public string BankName { get; set; }

         public string BankType { get; set;}

         [Required]
         public string AccountNumber { get; set; }

         public string UserId { get; set; }

         [ForeignKey("UserId")]
         public ApplicationUser applicationUser{ get; set; }

    }
}