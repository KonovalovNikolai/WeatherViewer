using OpenMeteoApi;

namespace WeatherViewer {
    public class AfterSunsetForecatsViewModel : HourForecastViewModelBase {
        public AfterSunsetForecatsViewModel(HourForecast hourForecast) : base(hourForecast) { }
        public override string IconSource => WeatherIconSelection.SelectNightIcon(WeatherCode);
    }
}
