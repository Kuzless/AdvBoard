using AdvBoard.Api.Configuration;
using AdvBoard.Application.CQRS.Announcement.Commands.AddAnnouncementCommand;
using AdvBoard.Application.Interfaces;
using AdvBoard.Application.Services;
using AdvBoard.Domain.Entities;
using AdvBoard.Domain.Interfaces;
using AdvBoard.Infrastructure;
using AdvBoard.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

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
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddAnnouncementCommand).Assembly));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddSwaggerGen(option =>
                {
                    option.SwaggerDoc("v1", new OpenApiInfo { Title = "AdvBoard" });
                    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });

                    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                    });
                });
            } else
            {
                builder.Services.AddSwaggerGen();
            }


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
