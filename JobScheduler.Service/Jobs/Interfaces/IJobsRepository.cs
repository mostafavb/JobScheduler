using Hangfire;

namespace JobScheduler.Service.Jobs.Interfaces;
public interface IJobsRepository
{
    Task Run(string queueName,IJobCancellationToken cancellationToken);
}
