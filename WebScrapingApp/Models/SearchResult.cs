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
        public SearchEngine SelectedSearchEngine { get; set; }
        public List<SearchResultItem> InfotrackResults { get; set; }
    }

    public class SearchResultItem
    {
        public int Position { get; set; }
    }
}
