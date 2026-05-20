using Invoicer.Domain.Entities;

namespace Invoicer.Application.Services;

public interface IJwtTokenGenerator
{
    AuthenticationResult GenerateToken(User user);
}
