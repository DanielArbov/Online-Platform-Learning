using System.ComponentModel.DataAnnotations;

namespace Talent;

public class Credentials
{

    [Required(ErrorMessage = "Missing user email")]
    [MinLength(2, ErrorMessage = "email cant be less than 2 chars")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+", ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Missing user password")]
    [MinLength(2, ErrorMessage = "password cant be less than 2 chars")]

    public string Password { get; set; } = null!;
}

