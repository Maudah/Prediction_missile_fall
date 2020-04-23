using PL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    public class GeoLocationAutoCompleteModel
    {
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public async Task<List<Result>> SearchLocation(string query)
        {
            var result = new List<Result>();
            const string appId = "51n1d12kkw6EasbuRYvc";
            const string appCode = "1HhHoOJH2-jUQBfI5bMfQQ";
            const string baseUrl = "https://places.cit.api.here.com/places/v1/autosuggest";
            var url =
                $"{baseUrl}?at=40.74917%2C-73.98529&q={query}&Accept-Language=en-US%2Cen%3Bq%3D0.9%2Che-IL%3Bq%3D0.8%2Che%3Bq%3D0.7&app_id={appId}&app_code={appCode}";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response == null) return result;
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = Welcome.FromJson(jsonString);
                result.AddRange(res.Results);
            }
            return result;
        }
    }
}
