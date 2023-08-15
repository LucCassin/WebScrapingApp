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
            var httpClient = _httpClientFactory.CreateClient();
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler { CookieContainer = cookieContainer };
            httpClient = new HttpClient(handler);

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36");

            var searchUrl = $"https://www.google.co.uk/search?num=100&q={Uri.EscapeDataString(model.SearchTerm)}";

            await Task.Delay(TimeSpan.FromSeconds(1)); // Need this delay to get past the google consent form

            var response = await httpClient.GetAsync(searchUrl);
            var content = await response.Content.ReadAsStringAsync();

            model.InfotrackResults = GetInfotrackResults(content);

            return View(model);
        }


        private List<SearchResultItem> GetInfotrackResults(string content)
        {
            var infotrackResults = new List<SearchResultItem>();
            var pattern = @"<cite.*?>(.*?)<\/cite>";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var matches = regex.Matches(content);

            int position = 1;

            foreach (Match match in matches)
            {
                var result = match.Groups[1].Value;
                if (result.Contains("www.infotrack.co.uk"))
                {
                    infotrackResults.Add(new SearchResultItem { Position = position });
                }

                position++;
            }

            return infotrackResults;
        }


    }
}
