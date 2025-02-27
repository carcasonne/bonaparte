namespace Bonaparte.Core.Common;

public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; }
    public string CreatedBy { get; }
    public DateTime UpdatedAt { get; set;}
    public string UpdatedBy { get; set;}
}