using Application.Shared.Ports.BackgroundJobs;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.BackgroundJobs;

internal sealed class BackgroundJobProcessor(IServiceProvider serviceProvider)
{
    public async Task ProcessCommand(
        string commandAssemblyQualifiedName,
        string commandJson,
        CancellationToken ct = default)
    {
        Type? commandType = Type.GetType(commandAssemblyQualifiedName);
        if (commandType == null)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                commandType = assembly.GetType(commandAssemblyQualifiedName);
                if (commandType != null)
                {
                    break;
                }
            }

            if (commandType == null)
            {
                throw new InvalidOperationException(
                    $"Could not find command type: {commandAssemblyQualifiedName}");
            }
        }

        var command = JsonConvert.DeserializeObject(commandJson, commandType);
        if (command == null)
        {
            throw new InvalidOperationException(
                $"Could not deserialize command to type {commandAssemblyQualifiedName}. JSON: {commandJson}");
        }

        var commandExecutor = serviceProvider.GetService<ICommandExecutor>();
        if (commandExecutor == null)
        {
            throw new InvalidOperationException(
                "ICommandExecutor service not found. Ensure it's registered in DI.");
        }

        var method = typeof(ICommandExecutor).GetMethod(
            nameof(ICommandExecutor.ExecuteCommandAsync),
            1,
            BindingFlags.Public | BindingFlags.Instance,
            null,
            [commandType],
            null);
        if (method == null)
        {
            throw new InvalidOperationException(
                $"ExecuteCommandAsync method for command type {commandType.Name} not found on ICommandExecutor.");
        }

        var genericMethod = method.MakeGenericMethod(commandType);
        await (Task)genericMethod.Invoke(commandExecutor, [command])!;
    }
}
