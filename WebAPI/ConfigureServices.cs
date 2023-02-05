using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;

namespace WebAPI;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        services.AddSingleton<ISynonymsRepository, SynonymsRepository>();
        services.AddScoped<ISynonymsService, SynonymsService>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
