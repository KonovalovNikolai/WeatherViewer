using OpenMeteoApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherViewer {
    public partial class MainPage : ContentPage {
        private ApplicationViewModel _viewModel;

        public MainPage() {
            InitializeComponent();
            _viewModel = new ApplicationViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing() {
            _ = _viewModel.GetForecast(47.21f, 38.94f);
        }
    }
}
