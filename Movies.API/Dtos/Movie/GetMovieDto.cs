using Movies.API.Dtos.Category;
using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Movie
{
    public class GetMovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //[DisplayFormat(DataFormatString = "DD/MM/yyyy")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; } 
        public string PosterUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public GetCategoryDto? Category { get; set; }
    }
}
