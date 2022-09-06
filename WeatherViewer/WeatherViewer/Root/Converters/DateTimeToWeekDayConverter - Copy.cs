﻿using OpenMeteoApi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace WeatherViewer.Converters {
    public class DateTimeToWeekDayConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var dateTime = (DateTime)value;
            return dateTime.ToString("ddd, dd MMM");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
