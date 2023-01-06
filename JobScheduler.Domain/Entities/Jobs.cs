using JobScheduler.Domain.Entities.Common;
using JobScheduler.Domain.Enums;

namespace JobScheduler.Domain.Entities; 
public class Jobs : BaseAuditableEntity
{
    public JobTypes JobType { get; set; }
    public string? Name { get; set; }
    public string? TypeName { get; set; }
    public required string TypeFullName { get; set; }
    public string? QueueName { get; set; }
    public string Cron { get; set; } = "30 10 * * 1-5";
}
