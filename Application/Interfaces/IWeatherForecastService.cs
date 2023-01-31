using Domain.Models;

namespace Application.Interfaces;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get();
}
