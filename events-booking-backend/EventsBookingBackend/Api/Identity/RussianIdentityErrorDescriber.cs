using Microsoft.AspNetCore.Identity;

namespace EventsBookingBackend.Api.Identity;

public class RussianIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => new IdentityError
        { Code = nameof(DefaultError), Description = "Произошла неизвестная ошибка." };

    public override IdentityError ConcurrencyFailure() => new IdentityError
        { Code = nameof(ConcurrencyFailure), Description = "Ошибка параллелизма, объект был изменен." };

    public override IdentityError PasswordMismatch() => new IdentityError
        { Code = nameof(PasswordMismatch), Description = "Неправильный пароль." };

    public override IdentityError InvalidToken() => new IdentityError
        { Code = nameof(InvalidToken), Description = "Недействительный токен." };

    public override IdentityError LoginAlreadyAssociated() => new IdentityError
        { Code = nameof(LoginAlreadyAssociated), Description = "Этот логин уже связан с другим пользователем." };

    public override IdentityError InvalidUserName(string userName) => new IdentityError
        { Code = nameof(InvalidUserName), Description = $"Имя пользователя '{userName}' недействительно." };

    public override IdentityError InvalidEmail(string email) => new IdentityError
        { Code = nameof(InvalidEmail), Description = $"Email '{email}' недействителен." };

    public override IdentityError DuplicateUserName(string userName) => new IdentityError
        { Code = nameof(DuplicateUserName), Description = $"Имя пользователя '{userName}' уже занято." };

    public override IdentityError DuplicateEmail(string email) => new IdentityError
        { Code = nameof(DuplicateEmail), Description = $"Email '{email}' уже используется." };

    public override IdentityError InvalidRoleName(string role) => new IdentityError
        { Code = nameof(InvalidRoleName), Description = $"Имя роли '{role}' недействительно." };

    public override IdentityError DuplicateRoleName(string role) => new IdentityError
        { Code = nameof(DuplicateRoleName), Description = $"Имя роли '{role}' уже занято." };

    public override IdentityError UserAlreadyHasPassword() => new IdentityError
        { Code = nameof(UserAlreadyHasPassword), Description = "Пользователь уже имеет пароль." };

    public override IdentityError UserLockoutNotEnabled() => new IdentityError
        { Code = nameof(UserLockoutNotEnabled), Description = "Блокировка не включена для этого пользователя." };

    public override IdentityError UserAlreadyInRole(string role) => new IdentityError
        { Code = nameof(UserAlreadyInRole), Description = $"Пользователь уже в роли '{role}'." };

    public override IdentityError UserNotInRole(string role) => new IdentityError
        { Code = nameof(UserNotInRole), Description = $"Пользователь не находится в роли '{role}'." };

    public override IdentityError PasswordTooShort(int length) => new IdentityError
        { Code = nameof(PasswordTooShort), Description = $"Пароль должен содержать не менее {length} символов." };

    public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError
    {
        Code = nameof(PasswordRequiresNonAlphanumeric),
        Description = "Пароль должен содержать хотя бы один неалфавитный символ."
    };

    public override IdentityError PasswordRequiresDigit() => new IdentityError
        { Code = nameof(PasswordRequiresDigit), Description = "Пароль должен содержать хотя бы одну цифру." };

    public override IdentityError PasswordRequiresLower() => new IdentityError
        { Code = nameof(PasswordRequiresLower), Description = "Пароль должен содержать хотя бы одну строчную букву." };

    public override IdentityError PasswordRequiresUpper() => new IdentityError
        { Code = nameof(PasswordRequiresUpper), Description = "Пароль должен содержать хотя бы одну заглавную букву." };
}