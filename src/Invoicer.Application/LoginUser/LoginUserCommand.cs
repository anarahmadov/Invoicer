using Invoicer.Application.Responses;
using Invoicer.Application.Services;
using MediatR;

namespace Invoicer.Application.LoginUser;

public class LoginUserCommand : IRequest<Result<AuthenticationResult>>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
