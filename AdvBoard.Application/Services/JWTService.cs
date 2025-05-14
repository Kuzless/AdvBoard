using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdvBoard.Application.CQRS.User.Commands;
using AdvBoard.Application.DTO.CommandDTOs;
using AdvBoard.Application.Interfaces;
using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace AdvBoard.Application.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        public JWTService(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _hostEnvironment = environment;
        }

        public string GenerateToken(AuthorizeUserGenerateTokenCommand user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            };
            SymmetricSecurityKey? key = null;
            if (_hostEnvironment.IsDevelopment())
            {
                key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            } else
            {
                var keyvault = new SecretClient(
                    new Uri($"https://{_configuration["KeyVault:KeyVaultName"]}.vault.azure.net/"),
                    new DefaultAzureCredential());
                var secret = keyvault.GetSecret(_configuration["KeyVault:Key"]).Value.Value.ToString();
                key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            }
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }
    }
}
