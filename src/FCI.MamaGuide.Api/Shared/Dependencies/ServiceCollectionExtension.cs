using FCI.MamaGuide.Api.Data;
using FCI.MamaGuide.Api.Domain.Base;
using FCI.MamaGuide.Api.Domain.Entities.Identity;
using FCI.MamaGuide.Api.Domain.Enums;
using FCI.MamaGuide.Api.Shared.Authentication.Jwt;
using FCI.MamaGuide.Api.Shared.Authentication.Settings;
using FCI.MamaGuide.Api.Shared.Behaviors;
using FCI.MamaGuide.Api.Shared.Infrastructure;
using FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;
using FCI.MamaGuide.Api.Shared.Utility;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace FCI.MamaGuide.Api.Shared.Dependencies;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(ConnectionString.DefaultConnection)
            ?? throw new ArgumentNullException(ConnectionString.DefaultConnection);

        services.AddSingleton(new ConnectionString(connectionString));

        services.AddDbContext<MamaGuideDbContext>(op =>
        {
            op.UseSqlServer(connectionString);
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        return services;
    }

    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        /* services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
         services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
         services.AddValidatorsFromAssemb(new[] { Assembly.GetExecutingAssembly() }, includeInternalTypes: true);*/

        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection AddIdentityUsers(this IServiceCollection services)
    {
        services.AddIdentity<BaseIdentityEntity, IdentityRole<Guid>>(op =>
        {
            op.Password.RequireDigit = true;
            op.Password.RequireLowercase = true;
            op.Password.RequireNonAlphanumeric = true;
            op.Password.RequireUppercase = true;
            op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            op.Lockout.MaxFailedAccessAttempts = 5;
            op.SignIn.RequireConfirmedPhoneNumber = true;
        })
            .AddEntityFrameworkStores<MamaGuideDbContext>()
            .AddDefaultTokenProviders();

        services.AddIdentityCore<Admin>()
           .AddRoles<IdentityRole<Guid>>()
           .AddEntityFrameworkStores<MamaGuideDbContext>()
           .AddDefaultTokenProviders();

        services.AddIdentityCore<Doctor>()
          .AddRoles<IdentityRole<Guid>>()
           .AddEntityFrameworkStores<MamaGuideDbContext>()
           .AddDefaultTokenProviders();

        services.AddScoped<UserUtility>();

        return services;
    }

    public static IServiceCollection AddAuthenticationSchema(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(op =>
        {
            op.DefaultAuthenticateScheme = "Default";
            op.DefaultChallengeScheme = "Default";
        })
            .AddJwtBearer("Default", op =>
            {
                var settings = configuration.GetSection(JwtSettings.SettingsKey).Get<JwtSettings>();
                var readKey = Encoding.ASCII.GetBytes(settings.Key);
                var key = new SymmetricSecurityKey(readKey);
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = key
                };
            });

        return services;
    }

    public static IServiceCollection AddAuthorizationPolices(this IServiceCollection services)
    {
        services.AddAuthorization(op =>
        {
            op.AddPolicy(nameof(AppRoles.Admin), policy => policy.RequireRole(nameof(AppRoles.Admin)));
            op.AddPolicy(nameof(AppRoles.Doctor), policy => policy.RequireRole(nameof(AppRoles.Doctor)));
            op.AddPolicy(nameof(AppRoles.User), policy => policy.RequireRole(nameof(AppRoles.User)));
        });

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingsKey));
        services.AddScoped<IJwtProvider, JwtProvider>();
        return services;
    }
}