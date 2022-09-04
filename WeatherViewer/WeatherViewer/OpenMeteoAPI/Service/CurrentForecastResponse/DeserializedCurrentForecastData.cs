using System.Text.Json.Serialization;

namespace OpenMeteoApi.Service {
    public class DeserializedCurrentForecastData {

        [JsonPropertyName("temperature")]
        public float Temperature { get; set; }

        [JsonPropertyName("weathercode")]
        public float WeatherCode { get; set; }

        public CurrentForecast Convert() {
            return new CurrentForecast(
                Temperature,
                (WeatherCode)WeatherCode
            );
        }
    }
}