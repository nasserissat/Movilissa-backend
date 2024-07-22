using Microsoft.AspNetCore.Identity;

namespace Movilissa_api.Infrastructure.Extensions;

public class CustomIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => new IdentityError
    {
        Code = nameof(DefaultError),
        Description = "Ha ocurrido un error no especificado."
    };

    public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError
    {
        Code = nameof(PasswordRequiresNonAlphanumeric),
        Description = "Las contraseñas deben tener al menos un carácter no alfanumérico."
    };

    public override IdentityError PasswordRequiresDigit() => new IdentityError
    {
        Code = nameof(PasswordRequiresDigit),
        Description = "Las contraseñas deben contener al menos un dígito ('0'-'9')."
    };

    public override IdentityError PasswordRequiresLower() => new IdentityError
    {
        Code = nameof(PasswordRequiresLower),
        Description = "Las contraseñas deben tener al menos una letra minúscula ('a'-'z')."
    };

    public override IdentityError PasswordRequiresUpper() => new IdentityError
    {
        Code = nameof(PasswordRequiresUpper),
        Description = "Las contraseñas deben tener al menos una letra mayúscula ('A'-'Z')."
    };
}
