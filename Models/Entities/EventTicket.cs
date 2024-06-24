using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Showdown_hub.Models.Enums;

namespace Showdown_hub.Models.Entities
{
    public class EventTicket
    {
        public string id { get; set; } =Guid.NewGuid().ToString();
         
         public string ticketName { get; set; }

         public TicketType ticketType{ get; set; }

         public Double  ticketPrice { get; set; }

        public bool isTicketSold { get; set; }

        public int  quantity { get; set; }

        public string eventId { get; set; }

        [ForeignKey("eventId")]
        public Events events{ get; set; }
        
    }
}