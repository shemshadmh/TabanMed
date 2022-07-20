
namespace Domain.Common
{
    public class EditableEntity : AuditableEntity
    {
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
