using System.ComponentModel.DataAnnotations;

namespace Movies.Common.Models.Dtos.Category
{
    public record CategoryToUpdateDto
    {
        [Required(ErrorMessage = "CategoryId is a required Field")]
        public int CategoryId { get; init; }

        [Required(ErrorMessage = "Name is a required field")]
        [MaxLength(25)]
        [MinLength(5)]
        public string? Name { get; init; }
    }
}
