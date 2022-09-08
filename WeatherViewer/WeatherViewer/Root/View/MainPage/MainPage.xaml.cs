using OpenMeteoApi;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeatherViewer.Root;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherViewer {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage {
        private ApplicationViewModel _viewModel;

        private Geolocator _geolocator;
        private ContentLoadController _dateForecastLoadController;
        private ErrorMessageController _errorMessageController;

        public MainPage() {
            InitializeComponent();
            _viewModel = new ApplicationViewModel();

            _geolocator = new Geolocator();
            _dateForecastLoadController = new ContentLoadController(DateForecastLoadIndicator, DateForecast);

            _errorMessageController = new ErrorMessageController(
                (val) => ErrorContainer.IsVisible = val,
                (mes) => ErrorLable.Text = mes
            );

            MainContent.IsVisible = false;
            _errorMessageController.Hide();
            _dateForecastLoadController.HideAll();

            RefreshContainer.Command = new Command(async () => {
                await LoadForecast();
                RefreshContainer.IsRefreshing = false;
            });

            BindingContext = _viewModel;
        }

        protected override void OnAppearing() {
            RefreshContainer.IsRefreshing = true;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e) {
            var item = e.Item as WeekDayForecast;
            _dateForecastLoadController.ShowLoadIndicator();

            try {
                await _viewModel.GetDateForecast(item.DateTime);
            }
            catch (Exception ex) {
                _dateForecastLoadController.HideAll();
                HandleConectionExeption(ex);
                return;
            }

            _dateForecastLoadController.ShowElement();
        }

        private async Task LoadForecast() {
            double longitude, latitude;
            try {
                (latitude, longitude) = await _geolocator.TryGetLocation();
            }
            catch (Exception ex) {
                HandleGeolocationException(ex);
                return;
            }

            var placemark = await _geolocator.GetPlacemark(latitude, longitude);

            _viewModel.Location = placemark.Locality;
            _viewModel.SetLocation(latitude, longitude);

            try {
                await _viewModel.GetForecast();
                await _viewModel.GetDateForecast(DateTime.Now);
            }
            catch (Exception ex) {
                HandleConectionExeption(ex);
                return;
            }

            _errorMessageController.Hide();
            MainContent.IsVisible = true;
            _dateForecastLoadController.ShowElement();
        }

        private void HandleGeolocationException(Exception exception) {
            switch (exception) {
                case FeatureNotSupportedException _:
                    _errorMessageController.SetMessage("Неудалось получить местоположение.\nГеолокация не поддерживается данным устройством");
                    break;
                case FeatureNotEnabledException _:
                    _errorMessageController.SetMessage("Неудалось получить местоположение.\nГеолокация выключена.");
                    break;
                case PermissionException _:
                    _errorMessageController.SetMessage("Неудалось получить местоположение.\nНеобходимо предоставить доступ к геолокации.");
                    break;
                default:
                    _errorMessageController.SetMessage("Неудалось получить местоположение.\nПопробуйте позже.");
                    break;
            }
        }

        private void HandleConectionExeption(Exception exception) {
            switch (exception) {
                case HttpRequestException _:
                    _errorMessageController.SetMessage("Неудалось загрузить прогноз.\nПопробуйте позже.");
                    break;
                case TaskCanceledException _:
                    _errorMessageController.SetMessage("Неудалось загрузить прогноз.\nПроверьте подключение к сети.");
                    break;
                default:
                    // При отсутствии подлючения, выбрасывается ни одно из верхних исключений,
                    // а Java.Net.UnknownHostException. Не знаю как отловить её. 😔
                    _errorMessageController.SetMessage("Неудалось загрузить прогноз.\nПроверьте подключение к сети.");
                    break ;
            }
        }

        protected override void OnDisappearing() {
            _geolocator.CancelGeolocationRequest();
            base.OnDisappearing();
        }
    }
}
