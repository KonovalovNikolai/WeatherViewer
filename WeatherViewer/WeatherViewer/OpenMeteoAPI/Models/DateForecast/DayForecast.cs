using System;

namespace OpenMeteoApi {
    public class DayForecast {
        public DayForecast(DateTime time, WeatherCode weather, float maxTemperature, float minTemperature, float precipitationSum, float windSpeed, float windDirection) {
            Time = time;
            Weather = weather;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            PrecipitationSum = precipitationSum;
            WindSpeed = windSpeed;
            WindDirection = windDirection;
        }

        public DateTime Time { get; private set; }
        public WeatherCode Weather { get; private set; }
        public float MaxTemperature { get; private set; }
        public float MinTemperature { get; private set; }
        public float PrecipitationSum { get; private set; }
        public float WindSpeed { get; private set; }
        public float WindDirection { get; private set; }
    }
}