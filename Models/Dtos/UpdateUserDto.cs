
namespace Showdown_hub.Models.Dtos
{
    public class UpdateUserDto
    {
        public string FirstName {get; set;}

       public string LastName {get; set;}

       public string PhoneNumber {get; set;}

       public int Gender {get; set;}

       public string Address {get; set;}

        public static implicit operator UpdateUserDto(bool v)
        {
            throw new NotImplementedException();
        }
    }
}