using Accounting.Infrastructures.Jobs;
using Framework.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Infrastructures.Configs;

public static class MessageDispatcherConfig
{
    public static IServiceCollection RegisterMessageDispatcher(
        this IServiceCollection services)
    {
        services.AddSingleton<IMessageDispatcher, MessageDispatcher>();

        return services;
    }
}