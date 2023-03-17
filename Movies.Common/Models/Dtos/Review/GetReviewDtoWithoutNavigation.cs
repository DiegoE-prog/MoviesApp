namespace Movies.Common.Models.Dtos.Review
{
    public record GetReviewDtoWithoutNavigation
    {
        public int ReviewId { get; init; }
        public string? ReviewText { get; init; }
        public DateTime ReviewDate { get; init; }
    }
}
