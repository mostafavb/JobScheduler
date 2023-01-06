using System.ComponentModel.DataAnnotations;

namespace JobScheduler.Domain.Entities.Common;
public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }
    [MaxLength(20)]
    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }
    [MaxLength(20)]
    public string? LastModifiedBy { get; set; }
}
