using System.ComponentModel.DataAnnotations;

namespace Movies.WEB.Models.Dtos.Review
{
    public class ReviewDto
    {
        public string ReviewText { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
        public GetUserForReviewDto? User { get; set; }
        public GetMovieForReviewDto? Movie { get; set; }
    }
}
