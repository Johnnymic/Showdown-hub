using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Showdown_hub.Models;
using Showdown_hub.Models.Entities;

namespace Showdown_hub.Data.DbContext
{
    public class EventHubContext : IdentityDbContext<ApplicationUser>
    {
        
        public DbSet<Account> accounts { get; set; }

       public DbSet<Events> events { get; set; }

      public  DbSet<EventTicket> eventTickets{ get; set; }

      public EventHubContext(DbContextOptions options) : base(options)
      {
        
      }


    }
}