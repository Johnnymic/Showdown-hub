using Showdown_hub.Data.Reposiotry.Interface;
using Showdown_hub.Models;

namespace Showdown_hub.Data.Reposiotry.Implementation
{
    public class GenerateToken : IGenerateJwt

    {
        private read
        public Task<string> GenerateToken(ApplicationUser applicationUser)
        {
            return Task.FromResult("");
        }
    }
}