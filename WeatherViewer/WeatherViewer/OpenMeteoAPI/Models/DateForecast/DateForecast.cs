using System;

namespace OpenMeteoApi {
    public class DateForecast {
        public DateForecast(DateTime time, WeatherCodes weather, float maxTemperature, float minTemperature, float precipitationSum, float windSpeed, float windDirection, HourForecast[] hourlyForecasts) {
            Time = time;
            Weather = weather;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            PrecipitationSum = precipitationSum;
            WindSpeed = windSpeed;
            WindDirection = windDirection;
            HourlyForecasts = hourlyForecasts;
        }

        public DateTime Time { get; private set; }
        public WeatherCodes Weather { get; private set; }
        public float MaxTemperature { get; private set; }
        public float MinTemperature { get; private set; }
        public float PrecipitationSum { get; private set; }
        public float WindSpeed { get; private set; }
        public float WindDirection { get; private set; }

        public HourForecast[] HourlyForecasts { get; private set; }
    }
}