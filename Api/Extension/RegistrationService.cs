using Npgsql.Replication;
using Showdown_hub.Api.Services.Implementation;
using Showdown_hub.Api.Services.Interface;
using Showdown_hub.Data.Reposiotry.Implementation;
using Showdown_hub.Data.Reposiotry.Interface;

namespace Showdown_hub.Api.Extension
{
    public static class RegistrationService
    {
        public  static void ConfigurationService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IAccountService, AccountService> ();
        }
    }
}