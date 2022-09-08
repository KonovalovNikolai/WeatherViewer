using OpenMeteoApi;

namespace WeatherViewer {
    public class AfterSunriseForecatsViewModel : HourForecastViewModelBase {
        public AfterSunriseForecatsViewModel(HourForecast hourForecast) : base(hourForecast) { }
        public override string IconSource => WeatherIconSelection.SelectDayIcon(WeatherCode);
    }
}
