using MassTransit;

namespace K8S.DriverAchievementAPI.Extensions
{
    public static class ConfigureMassTransitService
    {
        public static IServiceCollection AddConfigureMassTransitService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["RabbitMQ:Host"], "/", c =>
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
