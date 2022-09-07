using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;
using System.Linq;

namespace WeatherViewer.Root {
    public class Geolocator {
        private CancellationTokenSource _CTS;

        public async Task<Placemark> GetPlacemark(double latitude, double longitude) {
            var placemarks = await Geocoding.GetPlacemarksAsync(new Location(latitude, longitude));
            return placemarks.FirstOrDefault();
        }

        public async Task<(double, double)> TryGetLocation() {
            Location location;
            location = await TryGetLastLocation();

            if (location == null) {
                location = await TryGetCurrentLocation();
            }

            if (location == null) throw new Exception();

            return (location.Latitude, location.Longitude);
        }
        
        public void CancelGeolocationRequest() {
            if (_CTS != null && !_CTS.IsCancellationRequested) _CTS.Cancel();
        }

        private async Task<Location> TryGetLastLocation() {
            Location location;
            location = await Geolocation.GetLastKnownLocationAsync();

            return location;
        }

        private async Task<Location> TryGetCurrentLocation() {
            Location location = null;
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            _CTS = new CancellationTokenSource();
            location = await Geolocation.GetLocationAsync(request, _CTS.Token);

            return location;
        }
    }
}
