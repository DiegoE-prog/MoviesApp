using Movies.Common.Models.Dtos.Movie;
using Movies.Common.Models.Dtos.Review;

namespace Movies.WEB.ViewModels
{
    public class DetailsViewModel
    {
        public GetMovieDto? Movie { get; set; }
        public List<GetReviewDto>? Reviews { get; set; }
        public bool IsAlreadyReviewed { get; set; } = false;
    }
}
