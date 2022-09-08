using System;

namespace WeatherViewer {
    public class SunsetViewModel : SunViewModelBase {
        public SunsetViewModel(DateTime sunriseTime) : base(sunriseTime) { }

        public override string IconSource => "Sunset.png";
    }
}
