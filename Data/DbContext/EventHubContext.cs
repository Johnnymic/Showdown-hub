using Microsoft.AspNetCore.Identity;
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

      public DbSet<Roles> roles { get; set;}

      public EventHubContext(DbContextOptions options) : base(options)
      {
        
      }

      public static void SeedRole(ModelBuilder modelBuilder)
      {
        modelBuilder.Entity<IdentityRole>().HasData(

            new IdentityRole(){ Name = "ADMIN" , ConcurrencyStamp= "1", NormalizedName="admin"},
             new IdentityRole(){ Name = "USER" , ConcurrencyStamp = "2", NormalizedName="USER"}


        );
      }


    }
}