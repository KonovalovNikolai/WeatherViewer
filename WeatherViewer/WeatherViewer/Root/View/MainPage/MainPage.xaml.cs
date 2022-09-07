using OpenMeteoApi;
using System;
using System.Diagnostics;
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
            Console.WriteLine(e.Item);
            var item = e.Item as WeekDayForecast;

            _dateForecastLoadController.ShowLoadIndicator();
            await _viewModel.GetDateForecast(item.DateTime);
            _dateForecastLoadController.ShowElement();
        }

        private async Task LoadForecast() {
            (double latitude, double longitude) = (0, 0);
            try {
                (latitude, longitude) = await _geolocator.TryGetLocation();
            }
            catch (Exception ex) {
                HandleException(ex);
                return;
            }

            var placemark = await _geolocator.GetPlacemark(latitude, longitude);

            _viewModel.Location = placemark.Locality;
            _viewModel.SetLocation(latitude, longitude);
            await _viewModel.GetForecast();
            await _viewModel.GetDateForecast(DateTime.Now);

            _errorMessageController.Hide();
            MainContent.IsVisible = true;
            _dateForecastLoadController.ShowElement();
        }

        private void HandleException(Exception exception) {
            switch (exception) {
                case FeatureNotSupportedException fnsEx:
                    _errorMessageController.SetMessage("Неудалось получить местоположение.\nГеолокация не поддерживается данным устройством");
                    break;
                case FeatureNotEnabledException fneEx:
                    _errorMessageController.SetMessage("Неудалось получить местоположение.\nГеолокация выключена.");
                    break;
                case PermissionException pEx:
                    _errorMessageController.SetMessage("Неудалось получить местоположение.\nНеобходимо предоставить доступ к геолокации.");
                    break;
                default:
                    _errorMessageController.SetMessage("Неудалось получить местоположение.\nПопробкйте позже.");
                    break;
            }
        }

        protected override void OnDisappearing() {
            _geolocator.CancelGeolocationRequest();
            base.OnDisappearing();
        }
    }
}
