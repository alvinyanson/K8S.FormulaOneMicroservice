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
            _logger.LogInformation("\n==========================================================================================");

            _logger.LogInformation($"[*] 🔥 🔥 🔥  Latest driver achievement received: {context.Message.ToString()}");
            
            _logger.LogInformation("==========================================================================================\n");
            
            return Task.CompletedTask;
        }
    }
}
