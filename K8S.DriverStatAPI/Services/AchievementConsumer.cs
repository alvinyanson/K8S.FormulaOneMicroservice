using K8S.Contracts;
using MassTransit;

namespace K8S.DriverStatAPI.Services
{
    public class AchievementConsumer : IConsumer<AchievementCreated>
    {
        private readonly ILogger<AchievementConsumer> _logger;
        public AchievementConsumer(ILogger<AchievementConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<AchievementCreated> context)
        {
            _logger.LogInformation(" [*] Message received RaceWins: {RaceWins} ,WorldChampionship: {WorldChampionship} ", context.Message.RaceWins, context.Message.WorldChampionship);
            return Task.CompletedTask;
        }
    }
}
