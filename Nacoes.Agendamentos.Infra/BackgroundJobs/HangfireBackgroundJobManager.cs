using Hangfire;
using Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;
using System.Linq.Expressions;

namespace Nacoes.Agendamentos.Infra.BackgroundJobs;

public sealed class HangfireBackgroundJobManager : IBackgroundJobManager
{
    public void Dispatch(Expression<Action> methodCall, string queue = "default")
    {
        BackgroundJob.Enqueue(queue, methodCall);
    }

    public void Dispatch<T>(Expression<Action<T>> methodCall, string queue = "default")
    {
        BackgroundJob.Enqueue(queue, methodCall);
    }

    public void Dispatch<T>(Expression<Func<T, Task>> methodCall, string queue = "default")
    {
        BackgroundJob.Enqueue(queue, methodCall);
    }

    public void Schedule<T>(string jobId, Expression<Action> methodCall, string cronExpression, string queue = "default")
    {
        RecurringJob.AddOrUpdate(jobId, queue, methodCall, cronExpression, new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Utc,
        });
    }

    public void Schedule<T>(string jobId, Expression<Action<T>> methodCall, string cronExpression, string queue = "default")
    {
        RecurringJob.AddOrUpdate(jobId, queue, methodCall, cronExpression, new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Utc,
        });    
    }

    public void Schedule<T>(string jobId, Expression<Func<T, Task>> methodCall, string cronExpression, string queue = "default")
    {
        RecurringJob.AddOrUpdate(jobId, queue, methodCall, cronExpression, new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Utc,
        });    
    }
}