using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenMeteoApi.Service
{
    public class DeserializedWeekForecastData
    {
        [JsonPropertyName("time")]
        public List<string> TimeArray { get; set; }

        [JsonPropertyName("weathercode")]
        public List<float> WeatherCodeArray { get; set; }

        [JsonPropertyName("temperature_2m_max")]
        public List<float> MaxTemperatureArray { get; set; }

        [JsonPropertyName("temperature_2m_min")]
        public List<float> MinTemperatureArray { get; set; }

        public WeekForecast Convert()
        {
            // Обрабатываем на один день меньше, из-за неплного прогноза на седьмой день,
            // Из-за чего ответ невозможно сериализовать
            int lenght = TimeArray.Count - 1;

            var daysArray = new WeekDayForecast[lenght];
            for (int i = 0; i < lenght; i++)
            {
                daysArray[i] = new WeekDayForecast(
                    (WeatherCodes)WeatherCodeArray[i],
                    DateTime.Parse(TimeArray[i]),
                    MaxTemperatureArray[i],
                    MinTemperatureArray[i]
                );
            }

            return new WeekForecast(daysArray);
        }
    }
}