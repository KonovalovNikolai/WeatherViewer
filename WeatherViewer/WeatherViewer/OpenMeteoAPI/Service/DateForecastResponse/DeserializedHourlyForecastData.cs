using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenMeteoApi.Service {
    public class DeserializedHourlyForecastData {
        [JsonPropertyName("time")]
        public List<string> TimeArray { get; set; }

        [JsonPropertyName("temperature_2m")]
        public List<float> TemperatureArray { get; set; }

        [JsonPropertyName("relativehumidity_2m")]
        public List<float> RelativeHumidityArray { get; set; }


        [JsonPropertyName("weathercode")]
        public List<float> WeatherCodeArray { get; set; }

        public HourForecast[] Convert() {
            var result = new HourForecast[TimeArray.Count];

            for (int i = 0; i < result.Length; i++) {
                result[i] = new HourForecast(
                    DateTime.Parse(TimeArray[i]),
                    TemperatureArray[i],
                    RelativeHumidityArray[i],
                    (WeatherCodes)WeatherCodeArray[i]
                );
            }

            return result;
        }
    }
}