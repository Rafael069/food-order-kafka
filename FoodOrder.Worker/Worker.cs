using Confluent.Kafka;

namespace FoodOrder.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "food-order-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe("tp-order");

            _logger.LogInformation("Kafka Consumer iniciado...");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(stoppingToken);

                    _logger.LogInformation("Mensagem recebida:");
                    _logger.LogInformation(result.Message.Value);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao consumir mensagem");
                }

                await Task.Delay(500, stoppingToken);
            }
        }
    }
}