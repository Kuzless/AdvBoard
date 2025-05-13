using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using AdvBoard.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AdvBoard.Api.Configuration
{
    public static class ConnectionConfiguration
    {
        public static void ConfigureAuth(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
           .AddCookie(opt =>
           {
               opt.Cookie.HttpOnly = true;
               opt.Cookie.SameSite = SameSiteMode.None;
               opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;

           })
           .AddGoogle(GoogleDefaults.AuthenticationScheme, opt =>
           {
               opt.SaveTokens = true;
               opt.Events.OnCreatingTicket = context =>
               {
                   var idToken = context.TokenResponse.Response?.RootElement
                       .GetProperty("id_token").GetString();

                   if (!string.IsNullOrEmpty(idToken))
                   {
                       var tokens = context.Properties.GetTokens().ToList();
                       tokens.Add(new AuthenticationToken
                       {
                           Name = "id_token",
                           Value = idToken
                       });
                       context.Properties.StoreTokens(tokens);
                   }
                   return Task.CompletedTask;
               };

               if (builder.Environment.IsProduction())
               {
                   var keyvault = new SecretClient(
                       new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
                       new DefaultAzureCredential());

                   opt.ClientId = keyvault.GetSecret("GoogleClientId").Value.Value.ToString();
                   opt.ClientSecret = keyvault.GetSecret("GoogleClientSecret").Value.Value.ToString();
               }
               else if (builder.Environment.IsDevelopment())
               {
                   opt.ClientId = builder.Configuration["GoogleKeys:ClientId"];
                   opt.ClientSecret = builder.Configuration["GoogleKeys:ClientSecret"];
               }
           });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("GoogleAuthenticated", policy =>
                    policy.RequireAuthenticatedUser()
                          .AddAuthenticationSchemes(GoogleDefaults.AuthenticationScheme));
            });
        }
        
        public static void ConfigureDbConnection(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                if (builder.Environment.IsProduction())
                {
                    var keyvault = new SecretClient(
                        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
                        new DefaultAzureCredential());

                    options.UseSqlServer(keyvault.GetSecret("DatabaseConStr").Value.Value.ToString());
                }
                else if (builder.Environment.IsDevelopment())
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
            });
        }
    }
}
