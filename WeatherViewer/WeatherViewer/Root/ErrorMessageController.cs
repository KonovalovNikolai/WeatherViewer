using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WeatherViewer {
    public class ErrorMessageController {
        public ErrorMessageController(Action<bool> toggleErrorVisibility, Action<string> setErrorText) {
            ToggleErrorVisibility = toggleErrorVisibility;
            SetErrorText = setErrorText;
        }

        private Action<bool> ToggleErrorVisibility;
        private Action<string> SetErrorText;

        public void SetMessage(string message) {
            ToggleErrorVisibility(true);
            SetErrorText(message);
        }

        public void Hide() {
            ToggleErrorVisibility(false);
        }
    }
}
