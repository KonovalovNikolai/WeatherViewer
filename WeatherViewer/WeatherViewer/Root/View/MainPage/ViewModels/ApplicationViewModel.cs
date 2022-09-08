using OpenMeteoApi;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace WeatherViewer {
    public class ApplicationViewModel : INotifyPropertyChanged {
        private double _latitude;
        private double _longitude;

        public event PropertyChangedEventHandler PropertyChanged;

        public CancellationTokenSource APICTS { get; private set; }

        private string _location;
        public string Location {
            get => _location;
            set {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private CurrentForecast _currentForecast;
        public CurrentForecast CurrentForecast {
            get => _currentForecast;
            set {
                _currentForecast = value;
                OnPropertyChanged(nameof(CurrentForecast));
            }
        }

        private WeekForecast _weekForecast;
        public WeekForecast WeekForecast {
            get => _weekForecast;
            set {
                _weekForecast = value;
                OnPropertyChanged(nameof(WeekForecast));
            }
        }

        private DateForecastViewModel _dateForecast;
        public DateForecastViewModel DateForecast {
            get => _dateForecast;
            set {
                _dateForecast = value;
                OnPropertyChanged(nameof(DateForecast));
            }
        }

        private bool _isBuisy;

        public void SetLocation(double latitude, double longitude) {
            _latitude = latitude;
            _longitude = longitude;
        }

        public async Task GetForecast() {
            if (_isBuisy) return;
            _isBuisy = true;

            CurrentForecast = await OpenMeteoAPI.GetCurrentWeatherAsync(_latitude, _longitude);
            WeekForecast = await OpenMeteoAPI.GetWeekForecastAsync(_latitude, _longitude);
            
            _isBuisy = false;
        }

        public async Task GetDateForecast(DateTime date) {
            if (_isBuisy)
                return;

            _isBuisy = true;
            var dateForecast = await OpenMeteoAPI.GetDateWeatherAsync(_latitude, _longitude, date);
            DateForecast = new DateForecastViewModel(dateForecast);
            _isBuisy = false;
        }

        private void OnPropertyChanged(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
