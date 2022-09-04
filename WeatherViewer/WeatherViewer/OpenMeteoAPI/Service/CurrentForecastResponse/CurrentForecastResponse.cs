using System.Text.Json.Serialization;

namespace OpenMeteoApi.Service {
    public class CurrentForecastResponse {
        [JsonPropertyName("current_weather")]
        public DeserializedCurrentForecastData CurrentForecastData { get; set; }
        public CurrentForecast ToModel() => CurrentForecastData.Convert();
    }
}