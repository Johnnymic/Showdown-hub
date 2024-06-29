using Showdown_hub.Models;

namespace Showdown_hub.Data.Reposiotry.Interface
{
    public interface IGenerateJwt
    {
         Task<string>GenerateJwtToken(ApplicationUser applicationUser);

    }
}