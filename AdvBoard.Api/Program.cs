using AdvBoard.Api.Configuration;
using AdvBoard.Application.CQRS.User.Commands.SignUpCommand;
using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;
using AdvBoard.Infrastructure;
using AdvBoard.Infrastructure.Configuration;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdvBoard.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // connection strings
            if (builder.Environment.IsProduction())
            {
                var keyvault = new SecretClient(
                    new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
                    new DefaultAzureCredential());
                builder.Services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(keyvault.GetSecret("DatabaseConStr").Value.Value.ToString()));
            }
            else
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            }

            // identity
            builder.Services.AddScoped<IUserStore<User>, CustomUserStore>();
            builder.Services.AddIdentity<User, IdentityRole>(cfg =>
                {
                    cfg.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<DatabaseContext>();

            // services
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AutoMappingProfile));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignUpCommand).Assembly));   

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
