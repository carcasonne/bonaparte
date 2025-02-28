using Bonaparte.Core.Identity;

namespace Bonaparte.Core.Common;

public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; set;}
    public string CreatedById { get; set;}
    public ApplicationUser CreatedBy { get; }
    public DateTime UpdatedAt { get; set;}
    public string UpdatedById { get; }
    public ApplicationUser UpdatedBy { get; set;}
}