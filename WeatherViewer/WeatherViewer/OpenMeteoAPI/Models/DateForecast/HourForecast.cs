using System;

namespace OpenMeteoApi {
    public class HourForecast {
        public HourForecast(DateTime time, float temperature, float relativeHumidity, WeatherCode weatherCode) {
            Time = time;
            Temperature = temperature;
            RelativeHumidity = relativeHumidity;
            WeatherCode = weatherCode;
        }

        public DateTime Time { get; private set; }
        public float Temperature { get; private set; }
        public float RelativeHumidity { get; private set; }
        public WeatherCode WeatherCode { get; private set; }
    }
}