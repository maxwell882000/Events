using EventsBookingBackend.Api.ControllerOptions.Auth;
using EventsBookingBackend.Api.Identity;
using EventsBookingBackend.Domain.Auth.Entities;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Category.Repositories;
using EventsBookingBackend.Domain.Event.Repositories;
using EventsBookingBackend.Domain.File.Repositories;
using EventsBookingBackend.Domain.Files.Services;
using EventsBookingBackend.Domain.Review.Repositories;
using EventsBookingBackend.Domain.User.Repositories;
using EventsBookingBackend.Infrastructure.Payment.Payme.Option;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Event;
using EventsBookingBackend.Infrastructure.Repositories.File;
using EventsBookingBackend.Infrastructure.Repositories.Booking;
using EventsBookingBackend.Infrastructure.Repositories.Category;
using EventsBookingBackend.Infrastructure.Repositories.Event;
using EventsBookingBackend.Infrastructure.Repositories.Review;
using EventsBookingBackend.Infrastructure.Repositories.User;
using EventsBookingBackend.Infrastructure.Services.File;
using EventsBookingBackend.Shared.Options.File;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsBookingBackend.DependencyInjections;

public static class InfrastructureDi
{
    public static void AddAuth(this IServiceCollection services)
    {
        services.AddIdentity<Auth, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<RussianIdentityErrorDescriber>();
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(BearerTokenDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });
        services
            .AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = BearerTokenDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = BearerTokenDefaults.AuthenticationScheme;
                cfg.DefaultSignInScheme = BearerTokenDefaults.AuthenticationScheme;
                cfg.DefaultScheme = BearerTokenDefaults.AuthenticationScheme;
                cfg.DefaultForbidScheme = BearerTokenDefaults.AuthenticationScheme;
            })
            .AddScheme<AuthenticationSchemeOptions, PaymeBasicAuthenticationHandler>(
                "Payme", null)
            .AddBearerToken();
    }

    public static void AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("Database:Postgresql:ConnectionString");
        Console.WriteLine($"Connection string: {connectionString}");
        services.AddDbContextPool<UserDbContext>(options =>
            options.UseNpgsql(connectionString,
                    x =>
                        x.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                            schema: "users"))
                .UseSnakeCaseNamingConvention());
        services.AddDbContextPool<ReviewDbContext>(options =>
            options.UseNpgsql(connectionString, x =>
                    x.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                        schema: "reviews"))
                .UseSnakeCaseNamingConvention());
        services.AddDbContextPool<EventDbContext>(options =>
        {
            options.UseNpgsql(connectionString, x =>
                    x.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                        schema: "events"))
                .UseSnakeCaseNamingConvention();
        });
        services.AddDbContextPool<CategoryDbContext>(options =>
            options.UseNpgsql(connectionString, x =>
                    x.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                        schema: "categories"))
                .UseSnakeCaseNamingConvention());
        services.AddDbContextPool<BookingDbContext>(options =>
            options.UseNpgsql(connectionString, x =>
                    x.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                        schema: "bookings"))
                .UseSnakeCaseNamingConvention());
        services.AddDbContextPool<AuthDbContext>(options =>
            options.UseNpgsql(connectionString, x =>
                    x.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                        schema: "auth"))
                .UseSnakeCaseNamingConvention());
        services.AddDbContextPool<FileDbContext>(options =>
            options.UseNpgsql(connectionString, x =>
                    x.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                        schema: "files"))
                .UseSnakeCaseNamingConvention());
        services.AddDbContextPool<PaymeDbContext>(options =>
            options.UseNpgsql(connectionString, x =>
                    x.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                        schema: "payme"))
                .UseSnakeCaseNamingConvention());
    }

    public static async Task UseMigration(this IHost app)
    {
        using (var scope = app.Services.CreateAsyncScope())
        {
            var services = scope.ServiceProvider;

            // Migrate UserDbContext
            using var userContext = services.GetRequiredService<UserDbContext>();
            userContext.Database.Migrate();

            // Migrate ReviewDbContext
            using var reviewContext = services.GetRequiredService<ReviewDbContext>();
            reviewContext.Database.Migrate();

            // Migrate EventDbContext
            using var eventContext = services.GetRequiredService<EventDbContext>();
            eventContext.Database.Migrate();

            // Migrate CategoryDbContext
            using var categoryContext = services.GetRequiredService<CategoryDbContext>();
            categoryContext.Database.Migrate();

            // Migrate BookingDbContext
            using var bookingContext = services.GetRequiredService<BookingDbContext>();
            bookingContext.Database.Migrate();

            // Migrate AuthDbContext
            using var authContext = services.GetRequiredService<AuthDbContext>();
            authContext.Database.Migrate();

            // Migrate FileDbContext
            using var fileContext = services.GetRequiredService<FileDbContext>();
            fileContext.Database.Migrate();

            // Migrate PaymeDbContext
            using var paymeContext = services.GetRequiredService<PaymeDbContext>();
            paymeContext.Database.Migrate();
        }
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        #region Booking

        services.AddTransient<IBookingTypeRepository, BookingTypeRepository>();
        services.AddTransient<IBookingOptionRepository, BookingOptionRepository>();
        services.AddTransient<IBookingRepository, BookingRepository>();
        services.AddTransient<IBookingLimitRepository, BookingLimitRepository>();
        services.AddTransient<IBookingEventRepository, BookingEventRepository>();

        #endregion

        #region Category

        services.AddTransient<ICategoriesRepository, CategoryRepository>();

        #endregion

        #region Event

        services.AddTransient<IEventRepository, EventRepository>();
        services.AddTransient<ILikedEventRepository, LikedEventRepository>();
        services.AddTransient<IEventAggregatedReviewRepository, EventAggregatedReviewRepository>();

        #endregion

        #region Review

        services.AddTransient<IReviewRepository, ReviewRepository>();
        services.AddTransient<IReviewAggregateRepository, ReviewAggregateRepository>();

        #endregion

        #region User

        services.AddTransient<IUserRepository, UserRepository>();

        #endregion

        #region File

        services.AddTransient<IFileDbRepository, FileDbDbRepository>();
        services.AddTransient<IFileSystemRepository, FileSystemRepository>();

        #endregion
    }

    public static void AddServices(this IServiceCollection services)
    {
        #region File

        services.AddTransient<IFileService, FileService>();

        #endregion
    }

    public static void AddInfraOptions(this IServiceCollection services, IConfiguration configuration)
    {
        #region File

        services.Configure<PaymeOption>(
            configuration.GetSection(nameof(PaymeOption)));
        services.Configure<FileOption>(
            configuration.GetSection(nameof(FileOption)));

        #endregion
    }
}