using Showdown_hub.Models.Enums;

namespace Showdown_hub.Models.Entities
{
    public class Events
    {
       public  string  Id { get; set; }
       public string title { get; set; }

       public string description { get; set; }

       public string organiser { get; set; }

       public EventType eventType{ get; set; }

       public string location { get; set; }

       public DateTime startDate{ get; set; }

       public DateTime endDate { get; set; }  

       public DateTime createdAt { get; set; }

       public bool isActive { get; set; }

       public ICollection<EventTicket> eventTickets { get; set; }

       




    }
}