using Movies.WEB.Models.Dtos.Review;

namespace Movies.WEB.Models.Dtos
{
    public class DetailsViewModel
    {
        public MovieDto? Movie { get; set; }
        public List<ReviewDto>? Reviews { get; set; }
        public bool IsAlreadyReviewed { get; set; } = false;
    }
}
