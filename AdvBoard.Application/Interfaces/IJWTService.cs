using AdvBoard.Application.CQRS.User.Commands;
namespace AdvBoard.Application.Interfaces
{
    public interface IJWTService
    {
        string GenerateToken(AuthorizeUserGenerateTokenCommand user);
    }
}
