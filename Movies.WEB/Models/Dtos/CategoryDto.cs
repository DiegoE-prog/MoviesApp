using System.ComponentModel.DataAnnotations;

namespace Movies.WEB.Models.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is a required field")]
        [MaxLength(25)]
        [MinLength(5)]
        public string Name { get; set; } = String.Empty;
    }
}
