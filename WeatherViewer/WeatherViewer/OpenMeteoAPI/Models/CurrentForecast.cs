namespace OpenMeteoApi {

    public class CurrentForecast {
        public CurrentForecast(float temperature, WeatherCode weatherCode) {
            Temperature = temperature;
            WeatherCode = weatherCode;
        }

        public float Temperature { get; private set; }
        public WeatherCode WeatherCode { get; private set; }
    }
}