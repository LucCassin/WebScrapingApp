using System;
using System.ComponentModel.DataAnnotations;

namespace WebScrapingApp.Data
{
    public class SearchRecord
    {
        [Key]
        public int Id { get; set; }
        public string SearchTerm { get; set; }
        public string SearchUrl { get; set; }
        public string SelectedSearchEngine { get; set; }
        public int PositionOfInitialOccurrence { get; set; }
        public DateTime DateSearched { get; set; }
    }
}
