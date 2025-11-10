using PlatformService.AsyncDataServices;

namespace PlatformService.Extensions
{
    public class RabbitMQInitializerHostedService : IHostedService
    {
        private readonly IMessageBusClient _messageBusClient;
        public RabbitMQInitializerHostedService
        (
            IMessageBusClient messageBusClient
        )
        {
            _messageBusClient = messageBusClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if(_messageBusClient is MessageBusClient concrete)
            {
                await concrete.InitializeConnectionFactory();
                Console.WriteLine("--> Initializing RabbitMQ");
            }
            else
            {
                Console.WriteLine("--> Cannot initialize RabbitMQ");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}