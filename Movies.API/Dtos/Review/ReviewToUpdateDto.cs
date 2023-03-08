using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Review
{
    public record ReviewToUpdateDto
    {
        [Required(ErrorMessage = "ReviewText is a required field")]
        public string? ReviewText { get; init; }
        [Required(ErrorMessage = "MovieId is a required field")]
        public int MovieId { get; init; }
    }
}
