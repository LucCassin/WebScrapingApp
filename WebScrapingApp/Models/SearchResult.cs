using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebScrapingApp.Models
{
    public enum SearchEngine
    {
        Google,
        Bing
    }

    public class SearchResult
    {
        [Key]
        public int SearchId { get; set; }

        [Required]
        public string SearchTerm { get; set; }

        [Required]
        public string SearchUrl { get; set; }

        [Required]
        public SearchEngine SelectedSearchEngine { get; set; }
        public List<SearchResultItem> SearchResults { get; set; }
    }


    public class SearchResultItem
    {
        [Key]
        public int PositionId { get; set; }

        public int Position { get; set; }

        public int SearchId { get; set; }
        public SearchResult SearchResult { get; set; }
    }
}
