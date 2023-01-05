using Hangfire;
using JobScheduler.Service.Jobs.Interfaces;
using JobScheduler.Service.Models;
using JobScheduler.Service.Providers;

namespace JobScheduler.Service.Services;

public class JobStarter : IJobStarter
{
    private readonly JobService jobService;
    private readonly AssemblyHelper assemblyHelper;

    public JobStarter()
        : this(new JobService(),new AssemblyHelper())
    {
    }
    public JobStarter(JobService jobService, AssemblyHelper assemblyHelper)
    {
        this.jobService = jobService;
        this.assemblyHelper = assemblyHelper;
    }
    public Task SetJobScheduler()
    {
        RecurringJob.AddOrUpdate("SettingJobs", 
            () => this.StartJobs(), 
            "0 10 * * 1", 
            TimeZoneInfo.FindSystemTimeZoneById("PST"),
            "general");

        RecurringJob.TriggerJob("SettingJobs");
        return Task.CompletedTask;
    }

    [JobDisplayName("Start Jobs")]
    public Task StartJobs()
    {
        Console.WriteLine($"Jobs is started at {DateTime.Now.ToLocalTime()} ...");

        var jobs = jobService.GetJobs();
        foreach (var job in jobs)
        {
            var type = assemblyHelper.GetInstanceOf<IJobsRepository>(job.TypeFullName);
            if (type != null)
                switch (job.JobType)
                {
                    case JobTypes.FireAndForget:

                        //BackgroundJob.Schedule(() => type.Run(new JobCancellationToken(true)),);
                       

                        break;
                    case JobTypes.Delayed:

                        break;
                    case JobTypes.Recurring:
                        RecurringJob.AddOrUpdate(job.Name,
                           () => type.Run(job.QueueName, JobCancellationToken.Null),
                           "*/1 * * * 1-5",
                           TimeZoneInfo.FindSystemTimeZoneById("PST"));
                        break;
                    default:
                        break;
                }

        }
        //Console.WriteLine($"Setting jobs finished at {DateTime.Now.ToLocalTime()}");
        return Task.CompletedTask;
    }  

}