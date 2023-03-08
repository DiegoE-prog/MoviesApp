using Movies.API.Dtos.Category;
using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Movie
{
    public record GetMovieDto
    {
        public int MovieId { get; init; }
        public string? Title { get; init; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; init; } 
        public string? PosterUrl { get; init; } 
        public int CategoryId { get; init; }
        public GetCategoryDto? Category { get; init; }
    }
}
