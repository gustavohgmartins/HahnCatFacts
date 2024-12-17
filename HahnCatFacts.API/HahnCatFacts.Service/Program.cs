using HahnCatFacts.Application.Interfaces.Services;
using HahnCatFacts.Application.Services;
using HahnCatFacts.Domain.Interfaces;
using HahnCatFacts.Infrastructure.Persistence.DataBaseContext;
using HahnCatFacts.Infrastructure.Persistence.Repositories;
using HahnCatFacts.Infrastructure.Services;
using HahnCatFacts.Service;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString, sqlServerAction =>
        {
            sqlServerAction.EnableRetryOnFailure(3);
            sqlServerAction.CommandTimeout(60);
        });
    });

    builder.Services.AddHttpClient<ICatFactSourceApiService, CatFactSourceApiService>(client =>
    {
        var configuration = builder.Configuration;
        var catFactUrl = configuration["ExternalApi:CatFactUrl"];
        client.BaseAddress = new Uri(catFactUrl);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

    builder.Services.AddHangfire(configuration => configuration.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));
    builder.Services.AddHangfireServer();

    builder.Services.AddHostedService<Worker>();

    builder.Services.AddScoped<ICatFactService, CatFactService>();
    builder.Services.AddScoped<ICatFactRepository, CatFactRepository>();
    builder.Services.AddScoped<ICatFactSourceApiService, CatFactSourceApiService>();

}

var host = builder.Build();
{
    await host.RunAsync();
}
