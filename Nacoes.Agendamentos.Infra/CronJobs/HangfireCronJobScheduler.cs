using Microsoft.Extensions.DependencyInjection;
using Nacoes.Agendamentos.Application.Abstracts.CronJobs;
using Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;

namespace Nacoes.Agendamentos.Infra.CronJobs;

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