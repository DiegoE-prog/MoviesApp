using System.ComponentModel.DataAnnotations;

namespace Movies.WEB.Models.Dtos
{
    public class MovieToUpdateDto
    {
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Title is a required field")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Synopsis is a required field")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Release Date is a required field")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Poster Url is a required field")]
        public string PosterUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "Category is a required field")]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
