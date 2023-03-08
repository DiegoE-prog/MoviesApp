using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Movie
{
    public record MovieToAddDto
    {
        [Required(ErrorMessage = "Title is a required field")]
        public string Title { get; init; } = string.Empty;

        [Required(ErrorMessage = "Description is a required field")]
        public string Description { get; init; } = string.Empty;

        [Required(ErrorMessage = "ReleaseDate is a required field")]
        public DateTime ReleaseDate { get; init; }

        [Required(ErrorMessage = "PosterUrl is a required field")]
        public string PosterUrl { get; init; } = string.Empty;

        [Required(ErrorMessage = "CategoryId is a required field")]
        public int CategoryId { get; init; }
    }
}
