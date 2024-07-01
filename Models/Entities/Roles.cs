using System.ComponentModel.DataAnnotations;

namespace Showdown_hub.Models.Entities
{
    public class Roles
    {
        [Key]
        public string id { get; set; } = Guid.NewGuid().ToString();

        public  string name { get; set; }


    }
}