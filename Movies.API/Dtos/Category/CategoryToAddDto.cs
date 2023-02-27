using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.Category
{
    public class CategoryToAddDto
    {
        [Required(ErrorMessage ="Name is a required field")]
        [MaxLength(25)]
        [MinLength(5)]
        public string Name { get; set; } = String.Empty;
    }
}
