using AdvBoard.Application.DTO;
using Microsoft.AspNetCore.Authentication;

namespace AdvBoard.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDTO> Authenticate(AuthenticateResult authResult);
    }
}
