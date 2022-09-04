using System.Text.Json.Serialization;

namespace OpenMeteoApi.Service {
    public class DateForecastResponse {
        [JsonPropertyName("hourly")]
        public DeserializedHourlyForecastData HourlyForecastData { get; set; }

        [JsonPropertyName("daily")]
        public DeserializedDayForecastData DayForecastData { get; set; }

        public DateForecast ToModel() => new DateForecast(DayForecastData.Convert(), HourlyForecastData.Convert());
    }
}