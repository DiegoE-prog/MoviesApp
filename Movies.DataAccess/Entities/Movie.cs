namespace Movies.DataAccess.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string? PosterUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
