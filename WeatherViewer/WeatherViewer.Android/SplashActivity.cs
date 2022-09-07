using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherViewer.Droid {
    [Activity(Label = "WeatherViewer", MainLauncher = true, Theme = "@style/Splash", NoHistory = true)]
    public class SplashActivity : Activity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume() {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity))); 
        }
    }
}