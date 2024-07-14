using System.Security.Principal;

namespace Showdown_hub.Models.Dtos
{
    public class PaginationDto
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPage { get; set; }

        public IEnumerable<PaginatedUserDto> userDtos { get; set; }
    }
}