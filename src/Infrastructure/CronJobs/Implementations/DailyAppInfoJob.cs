using Hangfire;
using Application.Shared.Ports.BackgroundJobs;
using Application.Shared.Ports.CronJobs;
using Application.Reports.Queries.InfoDiariaUsoApp;

namespace Infrastructure.CronJobs.Implementations;

internal sealed class DailyAppInfoJob : ICronJob
{
    public void Schedule(IBackgroundJobManager manager)
    {
        const string jobId = "daily-app-info";
        var cronExpression = Cron.Daily(hour: 0);
        const string queue = "admin";

        manager.Schedule<IQueryExecutor>(
            jobId,
            executor => executor.ExecuteQueryAsync<GetDailyAppUsageInfoQuery, GetDailyAppUsageInfoResponse>(
                new GetDailyAppUsageInfoQuery
            {
                SendByEmail = true
            }),
            cronExpression,
            queue);
    }
}
