using Hangfire;
using JobScheduler.Service.Jobs.Interfaces;

namespace JobScheduler.Service.Jobs.Implemantations;
public class SendEmail : IJobsRepository
{
    [AutomaticRetry(Attempts = 2, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
    [JobDisplayName("Send Email Display")]
    [Queue("{0}")]
    public Task Run(string queueName,IJobCancellationToken cancellationToken)
    {
        Console.WriteLine($"Sending email started at {DateTime.Now.ToLocalTime()}...");
        Task.Delay(TimeSpan.FromSeconds(3)).Wait();
        //throw new Exception("There is a problem in this job");
        Console.WriteLine($"Sending email finished at {DateTime.Now.ToLocalTime()}...");       
        return Task.CompletedTask;
    }
}
