using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Review
{
    public class GetReviewDtoWithoutNavigation
    {
        public int ReviewId { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        [DisplayFormat(DataFormatString = "DD/MM/yyyy")]
        public DateTime ReviewDate { get; set; }
    }
}
