using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Review
{
    public class GetReviewDto
    {
        public int ReviewId { get; init; }
        public string? ReviewText { get; init; } 
        public DateTime ReviewDate { get; init; }
        public GetUserForReviewDto? User { get; init; }
        public GetMovieForReviewDto? Movie { get; init; }
    }
}
