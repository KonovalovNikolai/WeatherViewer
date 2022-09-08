using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace WeatherViewer {
    public class HourTemplateSelector : DataTemplateSelector {
        public DataTemplate HourForecastTemplate { get; set; }

        public DataTemplate SunInfoTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            Console.WriteLine($"OnSelectTemplate: {item}");
            switch (item) {
                case HourForecastViewModelBase _: return HourForecastTemplate;
                case SunViewModelBase _: return SunInfoTemplate;
                default: return null;
            }
        }
    }
}
