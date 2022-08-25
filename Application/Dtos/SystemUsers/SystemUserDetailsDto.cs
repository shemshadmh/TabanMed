
namespace TabanAgency.Domain.Dtos.SystemUsers
{
    public class SystemUserDetailsDto
    {
        public SystemUserDetailsDto()
        {
            Roles = new List<SystemRoleListItemDto>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Username { get; set; }
        public List<SystemRoleListItemDto> Roles { get; set; }

    }
}
