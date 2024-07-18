using System.ComponentModel.DataAnnotations;

namespace Movilissa.core.DTOs.AUTH;

public class ResetPasswordData
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    [MinLength(8)]
    public string NewPassword { get; set; }

    [Required]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; }
}