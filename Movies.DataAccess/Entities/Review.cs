namespace Movies.DataAccess.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public bool IsActive { get; set; }
    }
}
