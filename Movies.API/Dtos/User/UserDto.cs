using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos.User
{
    public class UserDto
    {
        [Required(ErrorMessage = "Username is a required field")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is a required field")]
        public string Password { get; set; } = string.Empty;
    }
}
