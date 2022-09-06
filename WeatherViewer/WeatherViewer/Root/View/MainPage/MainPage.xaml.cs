using OpenMeteoApi;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WeatherViewer.Root;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherViewer {
    public partial class MainPage : ContentPage {
        private ApplicationViewModel _viewModel;

        private Geolocator _geolocator;
        private ContentLoadController _mainContentLoadController;
        private ContentLoadController _dateForecastLoadController;
        private ErrorMessageController _errorMessageController;


        public MainPage() {
            InitializeComponent();
            _viewModel = new ApplicationViewModel();

            _geolocator = new Geolocator();

            _mainContentLoadController = new ContentLoadController(ContentLoadIndicator, MainContent);
            _dateForecastLoadController = new ContentLoadController(DateForecastLoadIndicator, DateForecast);

            _mainContentLoadController.HideAll();
            _dateForecastLoadController.HideAll();

            _errorMessageController = new ErrorMessageController(
                (val) => ErrorContainer.IsVisible = val,
                (mes) => ErrorLable.Text = mes
            );

            BindingContext = _viewModel;
        }

        protected override void OnAppearing() {
            _ = LoadForecast();
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e) {
            Console.WriteLine(e.Item);
            var item = e.Item as WeekDayForecast;

            _dateForecastLoadController.ShowLoadIndicator();
            await _viewModel.GetDateForecast(item.DateTime);
            _dateForecastLoadController.ShowElement();
        }

        private async Task LoadForecast() {
            _mainContentLoadController.ShowLoadIndicator();

            (double latitude, double longitude) = (0, 0);
            try {
                (latitude, longitude) = await _geolocator.TryGetLocation();
            }
            catch (Exception ex) {
                HandleException(ex);
                return;
            }

            _viewModel.SetLocation(latitude, longitude);
            await _viewModel.GetForecast();
            await _viewModel.GetDateForecast(DateTime.Now);
            _mainContentLoadController.ShowElement();
            _dateForecastLoadController.ShowElement();
        }

        private void HandleException(Exception exception) {
            _mainContentLoadController.HideAll();
            Debug.WriteLine(exception.Message);
            switch (exception) {
                case FeatureNotSupportedException fnsEx:
                    _errorMessageController.SetMessage("Геолокация не поддерживается данным устройством");
                    break;
                case FeatureNotEnabledException fneEx:
                    _errorMessageController.SetMessage("Геолокация выключена");
                    break;
                case PermissionException pEx:
                    _errorMessageController.SetMessage("Необходимо предоставить доступ к геолокации");
                    break;
                default:
                    _errorMessageController.SetMessage("Неудалось получить местоположение. Попробкйте позже.");
                    break;
            }
        }

        protected override void OnDisappearing() {
            _geolocator.CancelGeolocationRequest();
            base.OnDisappearing();
        }
    }
}
