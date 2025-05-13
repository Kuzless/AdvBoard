using AdvBoard.MVC.Services;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace AdvBoard.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
            })
            .AddGoogle(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.ClientId = builder.Configuration["GoogleKeys:ClientId"];
                    options.ClientSecret = builder.Configuration["GoogleKeys:ClientSecret"];
                } else
                {
                    var keyvault = new SecretClient(
                        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
                        new DefaultAzureCredential());

                    options.ClientId = keyvault.GetSecret("GoogleClientId").Value.Value.ToString();
                    options.ClientSecret = keyvault.GetSecret("GoogleClientSecret").Value.Value.ToString();
                }

                    options.SaveTokens = true;
                options.Events.OnCreatingTicket = context =>
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
            });

            builder.Services.AddHttpClient<AnnouncementHttpService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["APIUrl"]!);
            });
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<AuthService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
