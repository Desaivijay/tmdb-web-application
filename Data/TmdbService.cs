using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace tmdb_web_application.Data
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.themoviedb.org/3";
        private const string ApiKey = "d98cb723a1040f1f77a3af31c07ac01e";

        public TmdbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Movie>> GetPopularMoviesAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/movie/popular?api_key={ApiKey}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TmdbApiResponse>(content);

            return result.Results;
        }
    }

    public class TmdbApiResponse
    {
        [JsonProperty("results")]
        public List<Movie> Results { get; set; }
    }

    public class Movie
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}

