using Accounting.Application;
using Accounting.Persistence.EF;
using Accounting.Persistence.EF.OutboxMessages;
using Autofac;
using Framework.Domain;
using Framework.Domain.Data;
using Framework.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Infrastructures.Configs;

public static class DependencyInjectionConfig
{
    public static ContainerBuilder RegisterRepository(
        this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterAssemblyTypes(typeof(PersistenceAssembly).Assembly)
                    .As(typeof(WriteRepository))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

        containerBuilder.RegisterAssemblyTypes(typeof(PersistenceAssembly).Assembly)
                    .As(typeof(ReadRepository))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

        return containerBuilder;
    }

    public static ContainerBuilder RegisterUnitOfWork(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType(typeof(EfUnitOfWork))
            .As(typeof(UnitOfWork))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return containerBuilder;
    }

    public static ContainerBuilder RegisterICommandHandler(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterAssemblyTypes(typeof(ApplicationAssembly).Assembly)
            .AsClosedTypesOf(typeof(ICommandHandler<,>))
            .InstancePerLifetimeScope();

        return containerBuilder;
    }

    public static ContainerBuilder RegisterMessageHandler(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterAssemblyTypes(typeof(ApplicationAssembly).Assembly)
            .AsClosedTypesOf(typeof(IHandleMessage<>))
            .InstancePerLifetimeScope();

        return containerBuilder;
    }

    public static ContainerBuilder RegisterOutboxManagement(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType(typeof(OutboxManagement))
            .As(typeof(IOutboxManagement))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return containerBuilder;
    }

    public static IServiceCollection RegisterDbContext(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContextFactory<ApplicationWriteDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddDbContextFactory<ApplicationReadDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}