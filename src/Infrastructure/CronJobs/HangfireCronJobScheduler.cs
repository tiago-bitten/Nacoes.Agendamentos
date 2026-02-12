using Microsoft.Extensions.DependencyInjection;
using Application.Shared.Ports.CronJobs;
using Application.Shared.Ports.BackgroundJobs;

namespace Infrastructure.CronJobs;

internal class HangfireCronJobScheduler(IServiceProvider serviceProvider,
                                        IBackgroundJobManager manager)
    : ICronJobScheduler
{
    public void ScheduleAll()
    {
        var jobs = serviceProvider.GetServices<ICronJob>();
        foreach (var job in jobs)
        {
            job.Schedule(manager);
        }
    }
}
