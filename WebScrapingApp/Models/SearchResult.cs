using System.Collections.Generic;

namespace WebScrapingApp.Models
{
    public enum SearchEngine
    {
        Google,
        Bing
    }

    public class SearchResult
    {
        public string SearchTerm { get; set; }
        public string SearchUrl { get; set; }
        public SearchEngine SelectedSearchEngine { get; set; }
        public List<SearchResultItem> SearchResults { get; set; }
    }


    public class SearchResultItem
    {
        public int Position { get; set; }
    }
}
