using Hangfire;
using Hangfire.SqlServer;
using JobScheduler.Server.Settings.Hnagfire;

namespace JobScheduler.Server.Extensions;

public static class HangfireServiceExtensions
{
    public static void AddHangfireService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
         .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
         .UseSimpleAssemblyNameTypeSerializer()
         .UseRecommendedSerializerSettings()
         .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
         {
             CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
             SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
             QueuePollInterval = TimeSpan.Zero,
             UseRecommendedIsolationLevel = true,
             DisableGlobalLocks = true
         }));

        // Add the processing server as IHostedService
        //services.AddHangfireServer();
    }


    public static void AddHagfireSettings(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<JobSettings>(configuration.GetSection("JobSettings"));

        var serverList = configuration.GetSection("HangfireSettings:ServerList").Get<List<HangfireServer>>();
        foreach (var server in serverList)
        {
            services.AddHangfireServer(srv =>
            {
                srv.ServerName = server.Name;
                srv.WorkerCount = server.WorkerCount;

                srv.CancellationCheckInterval = TimeSpan.FromSeconds(10);
                srv.Queues = server.QueueList;
            }
            );
        }
    }

    public static void UseHangfireDashbordConfig(this WebApplication app)
    {
        app.UseHangfireDashboard(
            "/hangfire", new DashboardOptions()
            {
                AppPath = "http://wesync/",
                DashboardTitle = "Scheduled Jobs",
                //  IsReadOnlyFunc = (DashboardContext context) => true, // If it is true, user can not delete/ enqueue the jobs.
                //Authorization = new[]
                //        {            
                //            new HangfireCustomBasicAuthenticationFilter
                //            {
                //                User = Configuration.GetSection("HangfireSettings:UserName").Value,
                //                Pass = Configuration.GetSection("HangfireSettings:Password").Value
                //            }
                //        }
            }
        );
    }
}
