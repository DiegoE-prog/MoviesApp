using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Review
{
    public class ReviewToUpdateDto
    {
        [Required(ErrorMessage = "ReviewText is a required field")]
        public string ReviewText { get; set; } = string.Empty;
        [Required(ErrorMessage = "MovieId is a required field")]
        public int MovieId { get; set; }
    }
}
