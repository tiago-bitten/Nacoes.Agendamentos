using System.Linq.Expressions;

namespace Application.Shared.Ports.BackgroundJobs;

public interface IBackgroundJobManager
{
    void Dispatch(Expression<Action> methodCall, string queue = "default");
    void Dispatch<T>(Expression<Action<T>> methodCall, string queue = "default");
    void Dispatch<T>(Expression<Func<T, Task>> methodCall, string queue = "default");

    void Schedule<T>(string jobId, Expression<Action> methodCall, string cronExpression, string queue = "default");
    void Schedule<T>(string jobId, Expression<Action<T>> methodCall, string cronExpression, string queue = "default");
    void Schedule<T>(string jobId, Expression<Func<T, Task>> methodCall, string cronExpression, string queue = "default");
}
