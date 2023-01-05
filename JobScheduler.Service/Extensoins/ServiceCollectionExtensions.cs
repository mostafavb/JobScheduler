using JobScheduler.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JobScheduler.Service.Extensoins;
public static class ServiceCollectionExtensions
{
    public static void AddSchedulerService(this IServiceCollection services)
    {
        services.AddSingleton<IJobStarter,JobStarter>();       

    }

    public static Task RunJobStarter(this IServiceProvider provider)
    {
        var jobStarter = provider.GetRequiredService<IJobStarter>();
        jobStarter.SetJobScheduler();
        return Task.CompletedTask;
    }


}
