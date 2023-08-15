using System.Collections.Generic;

namespace WebScrapingApp.Models
{
    public class SearchResult
    {
        public string SearchTerm { get; set; }
        public List<SearchResultItem> InfotrackResults { get; set; }
    }

    public class SearchResultItem
    {
        public int Position { get; set; }
    }

}
