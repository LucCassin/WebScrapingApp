using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebScrapingApp.Models;

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
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();
                var searchUrl = $"https://www.google.co.uk/search?num=100&q={Uri.EscapeDataString(model.SearchTerm)}";

                var response = await httpClient.GetAsync(searchUrl);
                var content = await response.Content.ReadAsStringAsync();

                model.InfotrackCount = CountInfotrackAppearances(content);
            }

            return View(model);
        }

        private int CountInfotrackAppearances(string content)
        {
            // Use regex to count occurrences of www.infotrack.co.uk
            var pattern = @"www\.infotrack\.co\.uk";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            var matches = regex.Matches(content);
            return matches.Count;
        }
    }
}
