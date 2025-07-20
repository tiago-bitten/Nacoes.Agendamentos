using System.Linq.Expressions;

namespace Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;

public interface IBackgroundJobDispatcher
{
    void Dispatch(Expression<Action> methodCall, string queue = "default");
    void Dispatch<T>(Expression<Action<T>> methodCall, string queue = "default");
    void Dispatch<T>(Expression<Func<T, Task>> methodCall, string queue = "default");
}