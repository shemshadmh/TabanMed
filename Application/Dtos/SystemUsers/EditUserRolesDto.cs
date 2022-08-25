
namespace TabanAgency.Domain.Dtos.SystemUsers
{
    public class EditUserRolesDto
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public List<SystemRoleListItemDto> Roles { get; set; }
        public List<string> UserRoles { get; set; }
    }
}
