using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Movie
{
    public class GetMovieWithoutCategoryDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [DisplayFormat(DataFormatString = "DD/MM/yyyy")]
        public DateTime ReleaseDate { get; set; }
        //public bool IsActive { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
