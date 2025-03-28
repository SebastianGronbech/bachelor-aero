using Gui.Core.Domain.Units;
using Gui.Core.Domain.Users;
using Gui.Core.SharedKernel;
using Gui.Infrastructure.Identity;
using Gui.Infrastructure.Persistence;
using Gui.Infrastructure.Persistence.Interceptors;
using Gui.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gui.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentity()
            .AddPersistence(configuration);

        // services.AddTransient<IDateTime, DateTimeService>();

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySqlConnection");

        services.AddDbContext<ApplicationContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                // b => b.MigrationsAssembly(typeof(GuiDbContext).Assembly.FullName))
                mySqlAction =>
                {
                    mySqlAction.EnableRetryOnFailure(3);
                    mySqlAction.CommandTimeout(60);
                }
                )
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());
        // .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information));

        // services.AddScoped<IApplicationDbContext>(provider => provider.GetService<GuiDbContext>());

        // services.AddTransient<IDateTime, DateTimeService>();

        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitRepository, UnitRepository>();
        services.AddScoped<IOperatorRepository, OperatorRepository>();
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationContext>());

        return services;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredUniqueChars = 1;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
        });

        return services;
    }
}