
namespace Domain.Entities.Permission
{
    public class Permission
    {
        public int Id { get; set; }

        public string Claim { get; set; } = null!;

        public string DisplayText { get; set; } = null!;

    }
}
