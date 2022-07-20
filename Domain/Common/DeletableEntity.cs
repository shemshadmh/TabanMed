﻿
namespace Domain.Common
{
    public abstract class DeletableEntity : EditableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
    }
}
