using System.ComponentModel.DataAnnotations;

namespace Movies.WEB.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
    }
}
