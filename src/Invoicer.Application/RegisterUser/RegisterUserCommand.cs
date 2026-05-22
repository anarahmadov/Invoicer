using Invoicer.Application.Responses;
using MediatR;

namespace Invoicer.Application.RegisterUser;

public class RegisterUserCommand : IRequest<Result>
{
    public string Fullname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}
