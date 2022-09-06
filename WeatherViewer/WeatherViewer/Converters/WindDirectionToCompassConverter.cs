using OpenMeteoApi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherViewer.Converters {
    public class WindDirectionToCompassConverter : IValueConverter {
        static string[] Compass = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            float direction = (float)value;
            int index = (int)((direction / 22.5f) + .5f) % 16;
            return Compass[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
