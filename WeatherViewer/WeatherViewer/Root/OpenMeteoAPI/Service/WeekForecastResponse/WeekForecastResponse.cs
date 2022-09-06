using System.Text.Json.Serialization;

namespace OpenMeteoApi.Service {
    public class WeekForecastResponse {
        [JsonPropertyName("daily")]
        public DeserializedWeekForecastData WeekWeatherData { get; set; }
        public WeekForecast ToModel() => WeekWeatherData.Convert();
    }
}