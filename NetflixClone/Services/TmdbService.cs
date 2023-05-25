
using NetflixClone.Models;
using System.Net.Http.Json;

namespace NetflixClone.Services
{
    public partial class TmdbService
    {
        private const string API_Key = "77a232acc57e43d472633e3a0bfd3570";
        public const string httpClientName = "TmdbClient";
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient httpClient;
        public TmdbService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            httpClient = _clientFactory.CreateClient(httpClientName);
        }
        
        async Task<IEnumerable<Media>> GetMediaAsync(string url)
        {
            var trendingMovies = await httpClient.GetFromJsonAsync<Movie>($"{url}&api_key={API_Key}");
            return trendingMovies.results.Select(r=>r.ToMediaObject());
        }
        public async Task<IEnumerable<Media>> GetTrendingAsync()=>await GetMediaAsync($"{TmdbUrls.trending }");
        public async Task<IEnumerable<Media>> GetNetflixOriginalsAsync() => await GetMediaAsync($"{TmdbUrls.netflixOriginals}");
        public async Task<IEnumerable<Media>> GetTopRatedAsync() => await GetMediaAsync($"{TmdbUrls.topRated}");
        public async Task<IEnumerable<Media>> GetActionAsync() => await GetMediaAsync($"{TmdbUrls.action}");

    }

    public static class TmdbUrls
    {
        public const string trending = "3/trending/all/week?language=en-US";
        public const string netflixOriginals = "3/discover/tv?language=en-US&with_networks=213";
        public const string topRated = "3/movie/top_rated?language=en-US";
        public const string action = "3/discover/movie?language=en-US&with_genres=28";

        public static string GetTrailers(int movieID, string type = "movie") => $"3/{type ?? "movie"}/{movieID}/videos?language=en-US";
        public static string GetMovieDetails(int movieID, string type = "movie") => $"3/{type ?? "movie"}?language=en-US";
        public static string GetSimilar(int movieID, string type = "movie") => $"3/{type ?? "movie"}/similar?language=en-US";
    }
}