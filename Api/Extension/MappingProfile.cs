using AutoMapper;
using Showdown_hub.Models;
using Showdown_hub.Models.Dtos;

namespace Showdown_hub.Api.Extension
{
    public class MappingProfile : Profile
    {
         public MappingProfile()
        {
            // Create maps between entities and DTOs here
            CreateMap<ApplicationUser, SignUpDto>();
            // Add other mappings here as needed
        }
    }
}