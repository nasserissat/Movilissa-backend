using System.ComponentModel.DataAnnotations;

namespace Movilissa.core.DTOs.AUTH;

public class LoginData
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}