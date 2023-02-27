namespace Movies.WEB.Models.Dtos.Review
{
    public class ReviewToUpdate
    {
        public int MovieId { get; set; }
        public string ReviewText { get; set; } = string.Empty;
    }
}
