namespace OpenMeteoApi {

    public class CurrentForecast {
        public CurrentForecast(float temperature, WeatherCodes weatherCode) {
            Temperature = temperature;
            WeatherCode = weatherCode;
        }

        public float Temperature { get; private set; }
        public WeatherCodes WeatherCode { get; private set; }
    }
}