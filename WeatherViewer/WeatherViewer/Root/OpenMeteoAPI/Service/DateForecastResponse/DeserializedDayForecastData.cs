using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenMeteoApi.Service {
    public class DeserializedDayForecastData {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; }

        [JsonPropertyName("weathercode")]
        public List<float> Weathercode { get; set; }

        [JsonPropertyName("temperature_2m_max")]
        public List<float> MaxTemperature { get; set; }

        [JsonPropertyName("temperature_2m_min")]
        public List<float> MinTemperature { get; set; }

        [JsonPropertyName("precipitation_sum")]
        public List<float> PrecipitationSum { get; set; }

        [JsonPropertyName("windspeed_10m_max")]
        public List<float> WindSpeed { get; set; }

        [JsonPropertyName("winddirection_10m_dominant")]
        public List<float> WindDirection { get; set; }

        public DayForecast Convert() {
            return new DayForecast(
                    DateTime.Parse(Time[0]),
                    (WeatherCodes)Weathercode[0],
                    MaxTemperature[0],
                    MinTemperature[0],
                    PrecipitationSum[0],
                    WindSpeed[0],
                    WindDirection[0]
                );
        }
    }
}