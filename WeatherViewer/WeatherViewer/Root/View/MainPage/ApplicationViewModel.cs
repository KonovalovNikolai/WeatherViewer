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

        private WeekDayForecast _selectedDay;
        public WeekDayForecast SelectedDay {
            get => _selectedDay;
            set { 
                _selectedDay = value;
                OnPropertyChanged(nameof(SelectedDay));
            }
        }

        private DateForecast _dateForecast;
        public DateForecast DateForecast {
            get => _dateForecast;
            set {
                _dateForecast = value;
                OnPropertyChanged(nameof(DateForecast));
            }
        }

        private bool _isBuisy;

        public async Task GetForecast(double latitude, double longitude) {
            if (_isBuisy) return;
            _isBuisy = true;

            _latitude = latitude;
            _longitude = longitude;

            CurrentForecast = await OpenMeteoAPI.GetCurrentWeatherAsync(latitude, longitude);
            WeekForecast = await OpenMeteoAPI.GetWeekForecastAsync(latitude, longitude);
            _selectedDay = WeekForecast.WeekDaysForecast[0];
            DateForecast = await OpenMeteoAPI.GetDateWeatherAsync(latitude, longitude, DateTime.Now);
            
            _isBuisy = false;
        }

        public async Task GetDateForecast(DateTime date) {
            if (_isBuisy)
                return;

            _isBuisy = true;
            DateForecast = await OpenMeteoAPI.GetDateWeatherAsync(_latitude, _longitude, date);
            _isBuisy = false;
        }

        private void OnPropertyChanged(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
