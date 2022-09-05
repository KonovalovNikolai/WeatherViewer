using OpenMeteoApi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace WeatherViewer.Converters {
    public class WeatherCodeToIconPathConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            const string ICON_FORMAT = ".png";
            string iconName = SelectIconName((WeatherCodes)value);
            return $"{iconName}{ICON_FORMAT}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        private string SelectIconName(WeatherCodes weatherCode) {
            switch (weatherCode) {
                case WeatherCodes.ClearSky:
                    return "Sunny";
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
