using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Review
{
    public class GetReviewDto
    {
        public int ReviewId { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        [DisplayFormat(DataFormatString = "DD/MM/yyyy")]
        public DateTime ReviewDate { get; set; }
        public GetUserForReviewDto? User { get; set; }
        public GetMovieForReviewDto? Movie { get; set; }
    }
}
