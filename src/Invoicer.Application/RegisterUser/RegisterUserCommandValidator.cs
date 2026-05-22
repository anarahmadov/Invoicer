using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Invoicer.Application.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator(IStringLocalizer<ValidationMessages> localizer)
    {
        RuleFor(x => x.Fullname)
            .NotEmpty()
            .WithMessage(localizer["FullnameRequired"])
            .WithErrorCode("FullnameRequired")
            .MaximumLength(100)
            .WithMessage(localizer["FullnameMaxLength"])
            .WithErrorCode("FullnameMaxLength");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(localizer["EmailRequired"])
            .WithErrorCode("EmailRequired")
            .EmailAddress()
            .WithMessage(localizer["EmailInvalid"])
            .WithErrorCode("EmailInvalid");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(localizer["PasswordRequired"])
            .WithErrorCode("PasswordRequired")
            .MinimumLength(8)
            .WithMessage(localizer["PasswordMinLength"])
            .WithErrorCode("PasswordMinLength")
            .Matches(@"[A-Z]")
            .WithMessage(localizer["PasswordUppercase"])
            .WithErrorCode("PasswordUppercase")
            .Matches(@"[a-z]")
            .WithMessage(localizer["PasswordLowercase"])
            .WithErrorCode("PasswordLowercase")
            .Matches(@"[0-9]")
            .WithMessage(localizer["PasswordDigit"])
            .WithErrorCode("PasswordDigit");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage(localizer["ConfirmPasswordRequired"])
            .WithErrorCode("ConfirmPasswordRequired")
            .Equal(x => x.Password)
            .WithMessage(localizer["PasswordsDoNotMatch"])
            .WithErrorCode("PasswordsDoNotMatch");
    }
}
