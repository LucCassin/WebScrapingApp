using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebScrapingApp.Models;
using System.Linq;

namespace WebScrapingApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SearchController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            var model = new SearchResult();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchResult model)
        {
            var validationResult = ValidateInput(model);
            if (!string.IsNullOrEmpty(validationResult))
            {
                ViewBag.ValidationError = validationResult;
                return View(model);
            }

            var httpClient = _httpClientFactory.CreateClient();
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler { CookieContainer = cookieContainer };
            httpClient = new HttpClient(handler);

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36");

            var searchUrl = GetSearchUrl(model.SelectedSearchEngine, model.SearchTerm);
            var response = await httpClient.GetAsync(searchUrl);
            var content = await response.Content.ReadAsStringAsync();

            model.SearchResults = GetSearchResults(content, model.SearchUrl);

            return View(model);
        }

        private string GetSearchUrl(SearchEngine searchEngine, string searchTerm)
        {
            switch (searchEngine)
            {
                case SearchEngine.Google:
                    return $"https://www.google.co.uk/search?num=100&q={Uri.EscapeDataString(searchTerm)}";
                case SearchEngine.Bing:
                    return $"https://www.bing.com/search?count=100&q={Uri.EscapeDataString(searchTerm)}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(searchEngine), "Invalid search engine selected.");
            }
        }

        private List<SearchResultItem> GetSearchResults(string content, string searchUrl)
        {
            var searchResults = new List<SearchResultItem>();
            var pattern = @"<cite.*?>(.*?)<\/cite>";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var matches = regex.Matches(content);

            int position = 1;

            foreach (Match match in matches)
            {
                var result = match.Groups[1].Value;
                if (result.Contains(searchUrl))
                {
                    searchResults.Add(new SearchResultItem { Position = position });
                }

                position++;
            }

            return searchResults;
        }

        private string ValidateInput(SearchResult model)
        {
            if (string.IsNullOrWhiteSpace(model.SearchTerm))
            {
                return "Search term is required.";
            }
            else if (string.IsNullOrWhiteSpace(model.SearchUrl))
            {
                return "URL is required.";
            }
            else if (!IsValidUrl(model.SearchUrl))
            {
                return "Invalid URL.";
            }

            return null;
        }

        private bool IsValidUrl(string input)
        {
            // Regular expression pattern to validate URLs
            string urlPattern = @"^www\.[\w\-]+\.(co(\.[a-z]{2,})?|com|edu|gov|mil|net|org|biz|info|mobi|name|aero|jobs|museum)$";

            return Regex.IsMatch(input, urlPattern, RegexOptions.IgnoreCase);
        }

    }
}
