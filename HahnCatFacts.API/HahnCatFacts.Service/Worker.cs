using HahnCatFacts.Worker.Jobs;
using Hangfire;

namespace HahnCatFacts.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            RecurringJob.AddOrUpdate<UpsertCatFactJob>(
                "UpsertCatFactJob",
                job => job.Run(stoppingToken),
                _configuration["Jobs:UpsertCatFactJob:cronExpression"]);

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
