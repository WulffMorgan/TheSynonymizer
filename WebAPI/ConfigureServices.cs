using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;

namespace WebAPI;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        _=services.AddControllers();
        _=services.AddSingleton<ISynonymsRepository, SynonymsRepository>();
        _=services.AddScoped<ISynonymsService, SynonymsService>();
        _=services.AddEndpointsApiExplorer();
        _=services.AddSwaggerGen();

        return services;
    }
}
