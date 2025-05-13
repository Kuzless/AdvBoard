using Microsoft.AspNetCore.Authentication;

namespace AdvBoard.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(AuthenticateResult authResult);
    }
}
