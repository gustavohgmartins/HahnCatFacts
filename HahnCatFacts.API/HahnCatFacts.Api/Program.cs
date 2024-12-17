using HahnCatFacts.Application.Interfaces.Services;
using HahnCatFacts.Application.Services;
using HahnCatFacts.Domain.Interfaces;
using HahnCatFacts.Infrastructure.Persistence.DataBaseContext;
using HahnCatFacts.Infrastructure.Persistence.Repositories;
using HahnCatFacts.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
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


    // Configure CORS
    builder.Services.AddCors(options =>
    {
        var corsAllowedOrigins = builder.Configuration["CorsSettings:AllowedOrigins"]?.Split(',');

        options.AddPolicy("AllowAnyOrigin", policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
    });

    builder.Services.AddHttpClient<ICatFactSourceApiService, CatFactSourceApiService>(client =>
    {
        var configuration = builder.Configuration;
        var catFactUrl = configuration["ExternalApi:CatFactUrl"];
        client.BaseAddress = new Uri(catFactUrl);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

    builder.Services.AddScoped<ICatFactRepository, CatFactRepository>();
    builder.Services.AddScoped<ICatFactService, CatFactService>();
    builder.Services.AddScoped<ICatFactSourceApiService, CatFactSourceApiService>();

    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseCors("AllowAnyOrigin");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}