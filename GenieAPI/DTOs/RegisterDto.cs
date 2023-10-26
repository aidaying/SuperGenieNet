using System.ComponentModel.DataAnnotations;

namespace GenieAPI.DTOs;

public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; }
    [Required]
    [EmailAddress] 
    public string Email { get; set; }
    [Required] 
    [RegularExpression("(?=^.{8,16}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 Number, 1 Non Alphanumeric and at least 8 Characters")]
    public string Password { get; set; }
}
