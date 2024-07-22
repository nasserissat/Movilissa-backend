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
        Description = "La contraseña debe contener al menos un carácter no alfanumérico."
    };

    public override IdentityError PasswordRequiresDigit() => new IdentityError
    {
        Code = nameof(PasswordRequiresDigit),
        Description = "La contraseña debe contener al menos un dígito ('0'-'9')."
    };

    public override IdentityError PasswordRequiresLower() => new IdentityError
    {
        Code = nameof(PasswordRequiresLower),
        Description = "La contraseña debe contener al menos una letra minúscula ('a'-'z')."
    };

    public override IdentityError PasswordRequiresUpper() => new IdentityError
    {
        Code = nameof(PasswordRequiresUpper),
        Description = "La contraseña debe contener al menos una letra mayúscula ('A'-'Z')."
    };

    public override IdentityError DuplicateUserName(string userName) => new IdentityError
    {
        Code = nameof(DuplicateUserName),
        Description = $"El nombre de usuario '{userName}' ya está en uso."
    };

    public override IdentityError DuplicateEmail(string email) => new IdentityError
    {
        Code = nameof(DuplicateEmail),
        Description = $"La dirección de correo electrónico '{email}' ya está registrada."
    };

    public override IdentityError InvalidEmail(string email) => new IdentityError
    {
        Code = nameof(InvalidEmail),
        Description = $"La dirección de correo electrónico '{email}' es inválida."
    };

    public override IdentityError ConcurrencyFailure() => new IdentityError
    {
        Code = nameof(ConcurrencyFailure),
        Description = "Error de concurrencia, el objeto ha sido modificado."
    };

    public override IdentityError PasswordTooShort(int length) => new IdentityError
    {
        Code = nameof(PasswordTooShort),
        Description = $"La contraseña debe ser de al menos {length} caracteres."
    };

    public override IdentityError PasswordMismatch() => new IdentityError
    {
        Code = nameof(PasswordMismatch),
        Description = "La confirmación de contraseña no coincide."
    };
}
