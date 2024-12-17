using HahnCatFacts.Application.Interfaces.Jobs;
using HahnCatFacts.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HahnCatFacts.Worker.Jobs
{
    public class UpsertCatFactJob : IUpsertCatFactJob
    {
        private readonly IServiceProvider _serviceProvider;

        public UpsertCatFactJob(IServiceProvider serviceProvider)
        {

            _serviceProvider = serviceProvider;
        }

        public async Task Run(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    ICatFactService scopedCatFactService = scope.ServiceProvider.GetRequiredService<ICatFactService>();

                    await scopedCatFactService.FetchAndAddNewCatFactAsync();
                }
            }
        }
    }
}
