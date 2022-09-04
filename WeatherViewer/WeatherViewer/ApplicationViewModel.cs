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

        private bool _isBuisy;

        public async Task GetCurrentForecast(float lat, float lon) {
            if (_isBuisy) return;
            _isBuisy = true;
            CurrentForecast = await OpenMeteoAPI.GetCurrentWeatherAsync(lat, lon);
            _isBuisy = false;
        }

        private void OnPropertyChanged(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
