using System;

namespace WeatherViewer {
    public abstract class SunViewModelBase : IHourViewModel {
        public DateTime Time { get; private set; }

        public SunViewModelBase(DateTime time) {
            Time = time;
        }

        public abstract string IconSource { get; }
    }
}
