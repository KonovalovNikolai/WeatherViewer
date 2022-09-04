using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;
using OpenMeteoApi.Service;

namespace OpenMeteoApi {
    public partial class OpenMeteoAPI {
        private const string BASE_URL = "https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}";

        public static async Task<WeekForecast> GetWeekForecastAsync(float latitude, float longitude) {
            const string ARGS = "&daily=weathercode,temperature_2m_max,temperature_2m_min&timezone=auto";

            string url = GetURL(latitude, longitude, ARGS);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<WeekForecastResponse>(content);
                return result.ToModel();
            }
            return null;
        }

        public static async Task<DateForecast> GetDateWeatherAsync(float latitude, float longitude, DateTime date) {
            const string ARGS =
                "&hourly=temperature_2m,relativehumidity_2m,weathercode" +
                "&daily=weathercode,temperature_2m_max,temperature_2m_min,precipitation_sum,windspeed_10m_max,winddirection_10m_dominant&timezone=auto" +
                "&start_date={0}&end_date={0}";

            string dateArg = string.Format(ARGS, date.ToString("yyyy-MM-dd"));
            string url = GetURL(latitude, longitude, string.Format(ARGS, dateArg));

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DateForecastResponse>(content);
                return result.ToModel();
            }

            return null;
        }

        public static async Task<CurrentForecast> GetCurrentWeatherAsync(float latitude, float longitude) {
            const string ARGS = "&current_weather=true&timezone=auto";

            string url = GetURL(latitude, longitude, ARGS);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CurrentForecastResponse>(content);
                return result.ToModel();
            }

            return null;
        }

        private static string GetURL(float latitude, float longitude, string url_args) {
            var url = string.Format(BASE_URL, 
                latitude.ToString(CultureInfo.InvariantCulture), 
                longitude.ToString(CultureInfo.InvariantCulture));
            return url + url_args;
        }
    }
}