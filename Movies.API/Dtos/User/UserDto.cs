using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.User
{
    public record UserDto
    {
        [Required(ErrorMessage = "Username is a required field")]
        public string? Username { get; init; } 
        [Required(ErrorMessage = "Password is a required field")]
        public string? Password { get; init; }
    }
}
