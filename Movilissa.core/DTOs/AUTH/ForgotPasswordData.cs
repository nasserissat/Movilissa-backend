using System.ComponentModel.DataAnnotations;

namespace Movilissa.core.DTOs.AUTH;

public class ForgotPasswordData
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}