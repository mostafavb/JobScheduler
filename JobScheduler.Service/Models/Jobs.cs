namespace JobScheduler.Service.Models;
public class Jobs
{
    public int Id { get; set; }
    public JobTypes JobType { get; set; }
    public string Name { get; set; }
    public string TypeName { get; set; }
    public required string TypeFullName { get; set; }
    public string QueueName { get; set; }
}

public enum JobTypes
{
    FireAndForget = 1,
    Delayed,
    Recurring
}

public class JobService
{
    public IEnumerable<Jobs> GetJobs()
    => new List<Jobs>{
            new Jobs() {Id=1,JobType=JobTypes.Recurring, Name="Send Email",TypeName="SendEmail",TypeFullName="JobScheduler.Service.Jobs.Implemantations.SendEmail",QueueName="mail"},
            new Jobs() {Id=2,JobType=JobTypes.Recurring, Name="Transfer EDI820 to database",TypeName="Edi820",TypeFullName="JobScheduler.Service.Jobs.Implemantations.Edi820",QueueName="edi"},
        };

}