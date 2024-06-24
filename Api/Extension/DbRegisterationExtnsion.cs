using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Showdown_hub.Data.DbContext;
using Showdown_hub.Models;

namespace Showdown_hub.Api.Extension
{
    public  static class DbRegisterationExtnsion
    {
        public static void ConfigureDb(this IServiceCollection service, IConfiguration configuration)
        {
           service.AddIdentity<ApplicationUser, IdentityRole>() .AddEntityFrameworkStores<EventHubContext>().AddDefaultTokenProviders();
           service.AddDbContext<EventHubContext>(option => option.UseNpgsql(configuration["EventHubDb"]));
        } 
    }
}