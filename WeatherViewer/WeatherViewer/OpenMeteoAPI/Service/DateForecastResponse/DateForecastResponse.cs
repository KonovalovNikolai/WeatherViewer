using System.Text.Json.Serialization;

namespace OpenMeteoApi.Service {
    public class DateForecastResponse {
        [JsonPropertyName("hourly")]
        public DeserializedHourlyForecastData HourlyForecastData { get; set; }

        [JsonPropertyName("daily")]
        public DeserializedDayForecastData DayForecastData { get; set; }

        public DateForecast ToModel() {
            var houlryForecast = HourlyForecastData.Convert();
            var dayForecast = DayForecastData.Convert();

            return new DateForecast(
                dayForecast.Time,
                dayForecast.SunriseTime,
                dayForecast.SunsetTime,
                dayForecast.Weather,
                dayForecast.MaxTemperature,
                dayForecast.MinTemperature,
                dayForecast.PrecipitationSum,
                dayForecast.WindSpeed,
                dayForecast.WindDirection,
                houlryForecast
            );
        }
    }
}