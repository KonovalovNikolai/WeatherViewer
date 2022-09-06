using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WeatherViewer {
    public class ContentLoadController {
        public ContentLoadController(ActivityIndicator activityIndicator, VisualElement element) {
            _activityIndicator = activityIndicator;
            _element = element;
        }

        private ActivityIndicator _activityIndicator;
        private VisualElement _element;

        public void ShowLoadIndicator() {
            ToggleIndicator(true);
            ToggleElement(false);
        }

        public void ShowElement() {
            ToggleElement(true);
            ToggleIndicator(false);
        }

        public void HideAll() {
            ToggleElement(false);
            ToggleIndicator(false);
        }

        private void ToggleIndicator(bool isActive) {
            _activityIndicator.IsVisible = isActive;
            _activityIndicator.IsRunning = isActive;
        }

        private void ToggleElement(bool isActive) {
            _element.IsVisible=isActive;
        }
    }
}
