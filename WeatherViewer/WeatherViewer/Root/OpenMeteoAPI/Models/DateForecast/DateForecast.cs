using System;

namespace OpenMeteoApi {
    public class DateForecast {
        public DateForecast(DateTime time, DateTime sunriseTime, DateTime sunsetTime, WeatherCodes weather, float maxTemperature, float minTemperature, float precipitationSum, float windSpeed, float windDirection, HourForecast[] hourlyForecasts) {
            Time = time;
            SunriseTime = sunriseTime;
            SunsetTime = sunsetTime;
            Weather = weather;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            PrecipitationSum = precipitationSum;
            WindSpeed = windSpeed;
            WindDirection = windDirection;
            HourlyForecast = hourlyForecasts;
        }

        public DateTime Time { get; private set; }
        public DateTime SunriseTime { get; private set; }
        public DateTime SunsetTime { get; private set; }
        public WeatherCodes Weather { get; private set; }
        public float MaxTemperature { get; private set; }
        public float MinTemperature { get; private set; }
        public float PrecipitationSum { get; private set; }
        public float WindSpeed { get; private set; }
        public float WindDirection { get; private set; }

        public HourForecast[] HourlyForecast { get; private set; }
    }
}