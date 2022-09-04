using System;

namespace OpenMeteoApi {
    public class WeekDayForecast {
        public WeekDayForecast(WeatherCode weather, DateTime dateTime, float maxTemperature, float minTemperature) {
            Weather = weather;
            DateTime = dateTime;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
        }

        public WeatherCode Weather { get; private set; }
        public DateTime DateTime { get; private set; }
        public float MaxTemperature { get; private set; }
        public float MinTemperature { get; private set; }
    }
}
