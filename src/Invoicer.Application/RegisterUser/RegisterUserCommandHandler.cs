using Invoicer.Application.Responses;
using Invoicer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Invoicer.Application.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
{
    private readonly UserManager<User> _userManager;

    public RegisterUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            Fullname = request.Fullname,
            CreatedDate = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return Result.Success();
        }

        var errors = string.Join("; ", result.Errors.Select(e => e.Description));
        return Result.Failure(errors);
    }
}
