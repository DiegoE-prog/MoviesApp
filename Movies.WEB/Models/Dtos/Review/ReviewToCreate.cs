namespace Movies.WEB.Models.Dtos.Review
{
    public class ReviewToCreate
    {
        public int MovieId { get; set; }
        public string ReviewText { get; set; } = string.Empty;
    }
}
