using JobScheduler.Domain.Entities;
using JobScheduler.Domain.Enums;

namespace JobScheduler.Service.Services;

public class JobService
{
    public IEnumerable<Domain.Entities.Jobs> GetJobs()
    => new List<Domain.Entities.Jobs>{
            new Domain.Entities.Jobs() {Id=1,JobType=JobTypes.Recurring, Name="Send Email",TypeName="SendEmail",TypeFullName="JobScheduler.Service.Jobs.Implemantations.SendEmail",QueueName="mail"},
            new Domain.Entities.Jobs() {Id=2,JobType=JobTypes.Recurring, Name="Transfer EDI820 to database",TypeName="Edi820",TypeFullName="JobScheduler.Service.Jobs.Implemantations.Edi820",QueueName="edi"},
        };

}