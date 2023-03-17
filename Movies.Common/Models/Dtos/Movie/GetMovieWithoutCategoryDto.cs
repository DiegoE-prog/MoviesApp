namespace Movies.Common.Models.Dtos.Movie
{
    public record GetMovieWithoutCategoryDto
    {
        public int MovieId { get; set; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public DateTime ReleaseDate { get; init; }
        public string? PosterUrl { get; init; } 
        public int CategoryId { get; init; }
    }
}
