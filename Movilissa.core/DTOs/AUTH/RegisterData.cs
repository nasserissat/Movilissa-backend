using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Movilissa.core.DTOs.AUTH;

public class RegisterData
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    public int? CompanyId { get; set; }
}
