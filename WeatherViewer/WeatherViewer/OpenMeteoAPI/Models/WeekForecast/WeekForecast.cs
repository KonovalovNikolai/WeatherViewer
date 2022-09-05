namespace OpenMeteoApi {
    public class WeekForecast {
        public WeekForecast(WeekDayForecast[] dayWeathers) {
            WeekDaysForecast = dayWeathers;
        }

        public WeekDayForecast[] WeekDaysForecast { get; private set; }
    }
}
