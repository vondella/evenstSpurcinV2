using System.Reflection;
using buildingblock.Behaviours;
using buildingblock.Config;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using buildingblock.Exceptions.Handler;
using user.cmd.api.Shared.Domains;
using user.cmd.api.Shared.Domains.Aggregates;
using user.cmd.api.Shared.Repository;
using user.cmd.api.Shared.Configuration;
using user.cmd.core.events;
using Carter;
using MassTransit;
using buildingblock.Middleware;

namespace user.cmd.api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiService(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();
        //services.AddHealthChecks();
        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            config.AddOpenBehavior(typeof(QueryCachingBehaviour<,>));

        });
        return services;
    }
    public static WebApplication UseApiServices(this WebApplication app)
    {
        //app.UseAuthentication();
        //app.UseAuthorization();
        app.MapCarter();
        app.UseExceptionHandler(options => { });
        return app;
    }
    public static IServiceCollection AddInfrastracture(this IServiceCollection services, IConfiguration configuration)
    {
        BsonClassMap.RegisterClassMap<BaseEvent>();
        BsonClassMap.RegisterClassMap<UserRegisteredEvent>();
        BsonClassMap.RegisterClassMap<UserUpdatedEvent>();
        BsonClassMap.RegisterClassMap<UserRemovedEvent>();
        services.Configure<MongoDbConfig>(configuration.GetSection(nameof(MongoDbConfig)));
        services.AddScoped<IEventStoreRepository, EventStoreRepository>();
        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped<IEventSourcingHandle<UserAggregates>, EventSourcingHandler>();
        services.AddMessageBroker(configuration);
        return services;
    }
    public static WebApplication UseRequestContextLogging(this WebApplication app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();
        return app;
    }

    private static IServiceCollection AddMessageBroker(this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
                config.AddConsumers(assembly);

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:UserName"]);
                    host.Password(configuration["MessageBroker:Password"]);
                });
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}