using OpenMeteoApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace WeatherViewer {
    public class ApplicationViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private CurrentForecast _currentForecast;
        public CurrentForecast CurrentForecast {
            get => _currentForecast;
            set {
                _currentForecast = value;
                OnPropertyChanged("CurrentForecast");
            }
        }

        private WeekForecast _weekForecast;
        public WeekForecast WeekForecast {
            get => _weekForecast;
            set {
                _weekForecast = value;
                OnPropertyChanged("WeekForecast");
            }
        }

        private bool _isBuisy;

        public async Task GetForecast(float lat, float lon) {
            if (_isBuisy) return;
            _isBuisy = true;
            CurrentForecast = await OpenMeteoAPI.GetCurrentWeatherAsync(lat, lon);
            WeekForecast = await OpenMeteoAPI.GetWeekForecastAsync(lat, lon);
            _isBuisy = false;
        }

        private void OnPropertyChanged(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
