using Hangfire;
using JobScheduler.Service.Jobs.Interfaces;

namespace JobScheduler.Service.Jobs.Implemantations;
public class Edi820 : IJobsRepository
{
    [AutomaticRetry(Attempts = 2, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
    [JobDisplayName("Transfer EDI820 Display")]
    [Queue("{0}")]
    public async Task Run(string queueName ,IJobCancellationToken jobCancellationToken)
    {
        Console.WriteLine($"Transfering EDI820 file to database {DateTime.Now.ToLocalTime()}...");        
    }
}
