using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Movie
{
    public class MovieToAddDto
    {
        [Required(ErrorMessage = "Title is a required field")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is a required field")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "ReleaseDate is a required field")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "PosterUrl is a required field")]
        public string PosterUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "CategoryId is a required field")]
        public int CategoryId { get; set; }
    }
}
