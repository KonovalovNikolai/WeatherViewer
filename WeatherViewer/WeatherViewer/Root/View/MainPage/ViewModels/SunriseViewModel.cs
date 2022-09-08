using System;

namespace WeatherViewer {
    public class SunriseViewModel : SunViewModelBase {
        public SunriseViewModel(DateTime sunriseTime) : base(sunriseTime) { }

        public override string IconSource => "Sunrise.png";
    }
}
