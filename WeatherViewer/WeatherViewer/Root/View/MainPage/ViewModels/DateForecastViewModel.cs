using OpenMeteoApi;
using System;
using System.ComponentModel;

namespace WeatherViewer {
    public class DateForecastViewModel {
        private DateForecast DateForecast { get; set; }

        public DateTime DateTime => DateForecast.Time;
        public WeatherCodes Weather => DateForecast.Weather;
        public float MaxTemperature => DateForecast.MaxTemperature;
        public float MinTemperature => DateForecast.MinTemperature;
        public float PrecipitationSum => DateForecast.PrecipitationSum;
        public float WindSpeed => DateForecast.WindSpeed;
        public float WindDirection => DateForecast.WindDirection;

        public DateForecastViewModel(DateForecast dateForecast) {
            DateForecast = dateForecast;
        }
    }
}
