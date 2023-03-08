using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Movie
{
    public record MovieToUpdateDto
    {
        [Required(ErrorMessage = "MovieId is a required field")]
        public int MovieId { get; init; }

        [Required(ErrorMessage = "Title is a required field")]
        public string? Title { get; init; } 

        [Required(ErrorMessage = "Description is a required field")]
        public string? Description { get; init; } 

        [Required(ErrorMessage = "ReleaseDate is a required field")]
        public DateTime ReleaseDate { get; init; }

        [Required(ErrorMessage = "PosterUrl is a required field")]
        public string? PosterUrl { get; init; }

        [Required(ErrorMessage = "CategoryId is a required field")]
        public int CategoryId { get; init; }
    }
}
