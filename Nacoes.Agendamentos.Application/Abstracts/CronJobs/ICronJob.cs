using Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;

namespace Nacoes.Agendamentos.Application.Abstracts.CronJobs;

public interface ICronJob
{
    void Schedule(IBackgroundJobManager manager);
}