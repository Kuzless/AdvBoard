using AdvBoard.Api.Configuration;
using AdvBoard.Application.CQRS.User.Commands.SignUpCommand;
using AdvBoard.Application.Interfaces;
using AdvBoard.Application.Services;
using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;
using AdvBoard.Infrastructure;
using AdvBoard.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;

namespace AdvBoard.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            ConnectionConfiguration.ConfigureAuth(builder);
            ConnectionConfiguration.ConfigureDbConnection(builder);

            // identity
            builder.Services.AddScoped<IUserStore<User>, CustomUserStore>();
            builder.Services.AddIdentity<User, IdentityRole>(cfg =>
                {
                    cfg.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<DatabaseContext>();

            // services
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAuthService, AuthService>();
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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
