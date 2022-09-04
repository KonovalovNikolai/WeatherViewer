namespace OpenMeteoApi {
    public class WeekForecast {
        public WeekForecast(WeekDayForecast[] dayWeathers) {
            DayWeathers = dayWeathers;
        }

        public WeekDayForecast[] DayWeathers { get; private set; }
    }
}
