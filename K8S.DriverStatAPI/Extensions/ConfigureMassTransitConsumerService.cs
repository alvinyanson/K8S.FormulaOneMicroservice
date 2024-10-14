using MassTransit;
using System.Reflection;

namespace K8S.DriverStatAPI.Extensions
{
    public static class ConfigureMassTransitConsumerService
    {
        public static IServiceCollection AddConfigureMassTransitConsumerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(m =>
            {
                m.AddConsumers(Assembly.GetExecutingAssembly());
                m.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["RabbitMq:Host"], "/", c =>
                    {
                        c.Username(configuration["RabbitMQ:Username"]!);
                        c.Password(configuration["RabbitMQ:Password"]!);
                    });
                    cfg.ConfigureEndpoints(ctx);
                });
            });
            return services;
        }
    }
}
