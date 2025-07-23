using Hangfire;
using Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;
using Nacoes.Agendamentos.Application.Abstracts.CronJobs;
using Nacoes.Agendamentos.Application.Abstracts.Notifications;
using Nacoes.Agendamentos.Application.Reports.Queries.InfoDiariaUsoApp;

namespace Nacoes.Agendamentos.Infra.CronJobs.Implementations;

internal sealed class DailyAppInfoJob : ICronJob
{
    public void Schedule(IBackgroundJobManager manager)
    {
        const string jobId = "daily-app-info";
        var cronExpression = Cron.Daily(hour: 0);
        const string queue = "admin";
        
        manager.Schedule<IQueryExecutor>(
            jobId,
            executor => executor.ExecuteQueryAsync<RecuperarInfoDiariaUsoAppQuery, RecuperarInfoDiariaUsoAppResponse>(
                new RecuperarInfoDiariaUsoAppQuery
            {
                EnviarPorEmail = true
            }),
            cronExpression,
            queue);
    }
}