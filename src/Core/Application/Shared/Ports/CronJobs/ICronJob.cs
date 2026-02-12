using Application.Shared.Ports.BackgroundJobs;

namespace Application.Shared.Ports.CronJobs;

public interface ICronJob
{
    void Schedule(IBackgroundJobManager manager);
}
