﻿using System;
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
        private ErrorMessageController _errorMessageController;


        public MainPage() {
            InitializeComponent();
            _viewModel = new ApplicationViewModel();

            _geolocator = new Geolocator();
            _mainContentLoadController = new ContentLoadController(ContentLoadIndicator, MainContent);

            _errorMessageController = new ErrorMessageController(
                (val) => ErrorContainer.IsVisible = val,
                (mes) => ErrorLable.Text = mes
            );

            BindingContext = _viewModel;
        }

        protected override void OnAppearing() {
            _ = LoadForecast();
        }

        private async Task LoadForecast() {
            _mainContentLoadController.ShowLoadIndicator();

            (double latitude, double longitude) = (0, 0);
            try {
                (latitude, longitude) = await _geolocator.TryGetLocation();
            }
            catch (Exception ex) {
                HandleGeolocationException(ex);
                return;
            }

            await _viewModel.GetForecast(latitude, longitude);
            _mainContentLoadController.ShowElement();
        }

        private void HandleGeolocationException(Exception exception) {
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
