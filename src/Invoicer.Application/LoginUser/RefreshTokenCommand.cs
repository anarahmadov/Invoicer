using Invoicer.Application.ResultPattern;
using Invoicer.Application.Services;
using MediatR;

namespace Invoicer.Application.LoginUser;

public class RefreshTokenCommand : IRequest<Result<AuthenticationResult>>
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
