using System.Security.Claims;
using AdvBoard.Application.Interfaces;
using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace AdvBoard.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Authenticate(AuthenticateResult authResult)
        {
            var claims = authResult.Principal?.Identities.FirstOrDefault()?.Claims;
            var token = authResult.Properties?.GetTokenValue("id_token");
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var id = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var existingUser = await _unitOfWork.UserManager.FindByIdAsync(id);

            if (existingUser == null)
            {
               await _unitOfWork.UserManager.CreateAsync(new User
               {
                   Id = id,
                   UserName = email,
                   Email = email,
                   Name = name,
                   AccessToken = token
               });
            } else
            {
                await _unitOfWork.UserManager.UpdateAsync(existingUser);
            }
            await _unitOfWork.SaveAsync();
            
            return token;
        }
    }
}
