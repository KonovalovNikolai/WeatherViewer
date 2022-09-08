using OpenMeteoApi;
using System;

namespace WeatherViewer {
    public abstract class HourForecastViewModelBase : IHourViewModel {
        private HourForecast HourForecast { get; set; }

        public abstract string IconSource { get; }

        public DateTime Time => HourForecast.Time;
        public float Temperature => HourForecast.Temperature;
        public float RelativeHumidity => HourForecast.RelativeHumidity;
        public WeatherCodes WeatherCode => HourForecast.WeatherCode;

        public HourForecastViewModelBase(HourForecast hourForecast) {
            HourForecast = hourForecast;
        }
    }
}
