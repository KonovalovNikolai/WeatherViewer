﻿using OpenMeteoApi;
using System;
using System.Diagnostics;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherViewer.ContentViews.ForecastIcon {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HourForecastIcon : ContentView {
        public WeatherCodes WeatherCode {
            get => (WeatherCodes)GetValue(WeatherCodeProperty);
            set => SetValue(WeatherCodeProperty, value);
        }
        public static readonly BindableProperty WeatherCodeProperty =
            BindableProperty.Create(nameof(WeatherCode), typeof(WeatherCodes), typeof(HourForecastIcon), WeatherCodes.ClearSky, BindingMode.OneTime);

        public DateTime Time {
            get => (DateTime)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }
        public static readonly BindableProperty TimeProperty =
            BindableProperty.Create(nameof(Time), typeof(DateTime), typeof(HourForecastIcon), default, BindingMode.OneTime);

        public HourForecastIcon() {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            Debug.WriteLine($"OnPropertyChanged: {propertyName}");

            if (propertyName is null || Icon is null)
                return;

            string iconPath = SelectIcon(WeatherCode, Time);
            Icon.Source = iconPath;
        }

        private static string SelectIcon(WeatherCodes weatherCodes, DateTime time) {
            const int NIGHTSTARTHOUR = 22;
            const int NIGHTENDHOUR = 5;

            string iconName;
            if (time.Hour >= NIGHTSTARTHOUR || time.Hour <= NIGHTENDHOUR) {
                iconName = SelectNightIcon(weatherCodes);
            }
            else {
                iconName = SelectDayIcon(weatherCodes);
            }
            
            return $"{iconName}.png";
        }

        private static string SelectNightIcon(WeatherCodes weatherCode) {
            switch (weatherCode) {
                case WeatherCodes.ClearSky:
                    return "ClearN";
                case WeatherCodes.MainlyClear:
                    return "MainlyClearN";
                case WeatherCodes.PartlyCloudy:
                    return "PartlyCloudyN";
                case WeatherCodes.LightDrizzle:
                    return "LightDrizzleN";
                case WeatherCodes.SlightRain:
                    return "SlightRainN";
                case WeatherCodes.SlightRainShower:
                case WeatherCodes.ModerateRainShower:
                    return "ShowerN";
                default:
                    return SelectDayIcon(weatherCode);
            }
        }

        private static string SelectDayIcon(WeatherCodes weatherCode) {
            switch (weatherCode) {
                case WeatherCodes.ClearSky:
                    return "Clear";
                case WeatherCodes.MainlyClear:
                    return "MainlyClear";
                case WeatherCodes.PartlyCloudy:
                    return "PartlyCloudy";
                case WeatherCodes.Overcast:
                    return "Overcast";
                case WeatherCodes.Fog:
                    return "Fog";
                case WeatherCodes.RimFog:
                    return "RimeFog";
                case WeatherCodes.LightDrizzle:
                    return "LightDrizzle";
                case WeatherCodes.ModerateDrizzle:
                case WeatherCodes.DenseDrizzle:
                    return "Drizzle";
                case WeatherCodes.LightFreezingDrizzle:
                case WeatherCodes.DenseFreezingDrizzle:
                    return "Snow";
                case WeatherCodes.SlightRain:
                    return "SlightRain";
                case WeatherCodes.ModerateRain:
                    return "ModerateRain";
                case WeatherCodes.HeavyRain:
                    return "HeavyRain";
                case WeatherCodes.LightFreezingRain:
                case WeatherCodes.HeavyFreezingRain:
                    return "Sleet";
                case WeatherCodes.SlightSnowFall:
                    return "Snow";
                case WeatherCodes.ModerateSnowFall:
                    return "Blizzard";
                case WeatherCodes.SnowGrains:
                    return "Hail";
                case WeatherCodes.SlightRainShower:
                case WeatherCodes.ModerateRainShower:
                    return "Shower";
                case WeatherCodes.ViolentRainShower:
                    return "HeavyRain";
                case WeatherCodes.SlightSnowShower:
                case WeatherCodes.HeavySnowShower:
                    return "Snow";
                case WeatherCodes.Thunderstorm:
                case WeatherCodes.SlightThunderstormHail:
                case WeatherCodes.HeavyThunderstormHail:
                    return "Thunderstorm";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}