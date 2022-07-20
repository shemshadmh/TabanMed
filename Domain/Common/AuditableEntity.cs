
namespace Domain.Common
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; } = null!;

        public DateTime Created { get; set; }

    }
}
