using OpenMeteoApi;
using System;
using System.Diagnostics;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherViewer.ContentViews.ForecastIcon {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HourForecastIcon : ContentView {
        // При создании элемента свойству присваивается значение по умолчанию,
        // что вызывает изменении иконки дважды и падение производительности.
        // nullable позволит проверить, было ли свойству присвоенно значение.
        public ForecastIconInfo? IconInfo {
            get => (ForecastIconInfo?)GetValue(IconInfoProperty);
            set => SetValue(IconInfoProperty, value);
        }
        public static readonly BindableProperty IconInfoProperty =
            BindableProperty.Create(nameof(IconInfo), typeof(ForecastIconInfo?), typeof(HourForecastIcon), null, BindingMode.OneTime);

        public HourForecastIcon() {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName is null)
                return;

            if (propertyName == nameof(IconInfo)) {
                if (!IconInfo.HasValue || Icon is null)
                    return;

                string iconPath = WeatherIconSelection.SelectIcon(IconInfo.Value.WeatherCode, IconInfo.Value.Time);
                Icon.Source = iconPath;
            }
        }
    }
}