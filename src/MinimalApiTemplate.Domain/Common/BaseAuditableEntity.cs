namespace MinimalApiTemplate.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset? LastModifiedDateTime { get; set; }

    public string? LastModifiedBy { get; set; }
}
