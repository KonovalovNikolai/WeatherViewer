namespace OpenMeteoApi {
    public class DateForecast {
        public DateForecast(DayForecast dayForecast, HourForecast[] hourlyForecasts) {
            DayForecast = dayForecast;
            HourlyForecasts = hourlyForecasts;
        }

        public DayForecast DayForecast { get; private set; }
        public HourForecast[] HourlyForecasts { get; private set; }
    }
}