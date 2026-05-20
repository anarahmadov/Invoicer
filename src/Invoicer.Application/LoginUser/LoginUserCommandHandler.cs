using Invoicer.Application.ResultPattern;
using Invoicer.Application.Services;
using Invoicer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Invoicer.Application.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthenticationResult>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserCommandHandler(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<AuthenticationResult>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result<AuthenticationResult>.Failure("Invalid email or password.");
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!passwordValid)
        {
            return Result<AuthenticationResult>.Failure("Invalid email or password.");
        }

        var authResult = _jwtTokenGenerator.GenerateToken(user);
        return Result<AuthenticationResult>.Success(authResult);
    }
}
