using Application.Interfaces;
using Application.Services;

namespace WebAPI;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
