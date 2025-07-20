using Hangfire;
using Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;
using System.Linq.Expressions;

namespace Nacoes.Agendamentos.Infra.BackgroundJobs;

public sealed class HangfireBackgroundJobDispatcher : IBackgroundJobDispatcher
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
}