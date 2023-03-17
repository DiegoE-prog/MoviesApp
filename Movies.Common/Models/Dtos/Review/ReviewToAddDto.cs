using System.ComponentModel.DataAnnotations;

namespace Movies.Common.Models.Dtos.Review
{
    public record ReviewToAddDto
    {
        [Required(ErrorMessage = "ReviewText is a required field")]
        public string ReviewText { get; init; } = string.Empty;
        [Required(ErrorMessage = "MovieId is a required field")]
        public int MovieId { get; init; }
    }
}
