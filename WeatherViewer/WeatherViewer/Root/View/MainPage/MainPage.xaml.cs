using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherViewer {
    public partial class MainPage : ContentPage {
        private ApplicationViewModel _viewModel;

        private ContentLoadController _mainContentLoadController;

        private CancellationTokenSource _geoLocationCTS;

        public MainPage() {
            InitializeComponent();
            _viewModel = new ApplicationViewModel();

            _mainContentLoadController = new ContentLoadController(ContentLoadIndicator, MainContent);

            BindingContext = _viewModel;
        }

        protected override void OnAppearing() {
            _ = LoadForecast();
        }

        private async Task LoadForecast() {
            _mainContentLoadController.ShowLoadIndicator();

            Location location;
            try {
                location = await TryGetLocation();
            }
            catch (Exception ex) {
                _mainContentLoadController.HideAll();
                HandleGeolocationException(ex);
                return;
            }

            await _viewModel.GetForecast(location.Latitude, location.Longitude);
            _mainContentLoadController.ShowElement();
        }


        private async Task<Location> TryGetLocation() {
            Location location;
            location = await TryGetLastLocation();

            if (location != null)
                return location;

            location = await TryGetCurrentLocation();
            return location;
        }

        private async Task<Location> TryGetLastLocation() {
            Location location;
            location = await Geolocation.GetLastKnownLocationAsync();

            return location;
        }

        private async Task<Location> TryGetCurrentLocation() {
            Location location = null;
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            _geoLocationCTS = new CancellationTokenSource();
            location = await Geolocation.GetLocationAsync(request, _geoLocationCTS.Token);

            return location;
        }

        private void HandleGeolocationException(Exception exception) {
            Debug.WriteLine(exception.Message);
            ErrorMessage.IsVisible = true;
            switch (exception) {
                case FeatureNotSupportedException fnsEx:
                    ErrorMessage.Text = "Геолокация не поддерживается данным устройством";
                    break;
                case FeatureNotEnabledException fneEx:
                    ErrorMessage.Text = "Геолокация выключена";
                    break ;
                case PermissionException pEx:
                    ErrorMessage.Text = "Разрешите приложение использовать геолокацию";
                    break;
                default:
                    ErrorMessage.Text = "Неудалось получить местоположение. Попробкйте позже";
                    break;
            }
        }

        protected override void OnDisappearing() {
            if (_geoLocationCTS != null && !_geoLocationCTS.IsCancellationRequested)
                _geoLocationCTS.Cancel();
            base.OnDisappearing();
        }
    }
}
