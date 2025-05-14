using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AdvBoard.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AdvBoard.Api.Configuration
{
    public static class ConnectionConfiguration
    {
        public static void ConfigureAuth(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                string key = "";
                if (builder.Environment.IsDevelopment()) { 
                    key = builder.Configuration["JWT:Key"]!;
                } else
                {
                    var keyvault = new SecretClient(
                        new Uri($"https://{builder.Configuration["KeyVault:KeyVaultName"]}.vault.azure.net/"),
                        new DefaultAzureCredential());
                    key = keyvault.GetSecret(builder.Configuration["KeyVault:Key"]).Value.Value.ToString();
                }

                options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key))
                    };
            });
        }
        
        public static void ConfigureDbConnection(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                if (builder.Environment.IsProduction())
                {
                    var keyvault = new SecretClient(
                        new Uri($"https://{builder.Configuration["KeyVault:KeyVaultName"]}.vault.azure.net/"),
                        new DefaultAzureCredential());

                    options.UseSqlServer(keyvault.GetSecret(builder.Configuration["KeyVault:Database"]).Value.Value.ToString(), opt =>
                    {
                        opt.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
                }
                else if (builder.Environment.IsDevelopment())
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
            });
        }
    }
}
