namespace HahnCatFacts.Application.Interfaces.Jobs
{
    public interface IBaseJob
    {
        Task Run(CancellationToken stoppingToken);
    }
}
