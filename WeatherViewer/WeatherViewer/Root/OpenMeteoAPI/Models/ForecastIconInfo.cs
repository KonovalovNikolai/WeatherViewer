using System;
using System.Collections.Generic;
using System.Text;

namespace OpenMeteoApi {
    public readonly struct ForecastIconInfo {
        public ForecastIconInfo(WeatherCodes weatherCode, DateTime time) {
            WeatherCode = weatherCode;
            Time = time;
        }

        public WeatherCodes WeatherCode { get; }
        public DateTime Time { get; }
    }
}
