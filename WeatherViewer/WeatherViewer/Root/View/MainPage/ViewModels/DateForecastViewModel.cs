using OpenMeteoApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WeatherViewer {
    public class DateForecastViewModel {
        private DateForecast DateForecast { get; set; }

        public DateTime Time => DateForecast.Time;
        public WeatherCodes Weather => DateForecast.Weather;
        public float MaxTemperature => DateForecast.MaxTemperature;
        public float MinTemperature => DateForecast.MinTemperature;
        public float PrecipitationSum => DateForecast.PrecipitationSum;
        public float WindSpeed => DateForecast.WindSpeed;
        public float WindDirection => DateForecast.WindDirection;

        private List<IHourViewModel> _hourlyForecast;
        public List<IHourViewModel> HourlyForecast {
            get {
                if (_hourlyForecast is null) {
                    BuildHourlyForecast();
                }

                return _hourlyForecast;
            }
        }

        public DateForecastViewModel(DateForecast dateForecast) {
            DateForecast = dateForecast;
        }

        private void BuildHourlyForecast() {
            _hourlyForecast = new List<IHourViewModel>();

            int index = 0;

            // Добавление элементов до времени рассвета
            for (; index < DateForecast.HourlyForecast.Length; index++) {
                var hourForecast = DateForecast.HourlyForecast[index];
                if (hourForecast.Time > DateForecast.SunriseTime)
                    break;
                _hourlyForecast.Add(new AfterSunsetForecatsViewModel(hourForecast));
            }

            _hourlyForecast.Add(new SunriseViewModel(DateForecast.SunriseTime));

            // Добавление элементов до времени заката
            for (; index < DateForecast.HourlyForecast.Length; index++) {
                var hourForecast = DateForecast.HourlyForecast[index];
                if (hourForecast.Time > DateForecast.SunsetTime)
                    break;
                _hourlyForecast.Add(new AfterSunriseForecatsViewModel(hourForecast));
            }

            _hourlyForecast.Add(new SunsetViewModel(DateForecast.SunsetTime));

            // Добавление оставшихся элементов
            for (; index < DateForecast.HourlyForecast.Length; index++) {
                var hourForecast = DateForecast.HourlyForecast[index];
                _hourlyForecast.Add(new AfterSunsetForecatsViewModel(hourForecast));
            }
        }
    }
}
