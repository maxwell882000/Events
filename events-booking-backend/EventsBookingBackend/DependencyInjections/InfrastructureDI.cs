using EventsBookingBackend.Api.ControllerOptions.Auth;
using EventsBookingBackend.Api.Identity;
using EventsBookingBackend.Domain.Auth.Entities;
using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Category.Repositories;
using EventsBookingBackend.Domain.Event.Repositories;
using EventsBookingBackend.Domain.File.Repositories;
using EventsBookingBackend.Domain.File.Services;
using EventsBookingBackend.Domain.Review.Repositories;
using EventsBookingBackend.Domain.User.Repositories;
using EventsBookingBackend.Infrastructure.Options.File;
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
using MassTransit;
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
        services.AddDbContextPool<TelegramDbContext>(options =>
            options.UseNpgsql(connectionString, x =>
                    x.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                        schema: "telegram"))
                .UseSnakeCaseNamingConvention());
    }

    public static async Task UseMigration(this IHost app)
    {
        using (var scope = app.Services.CreateAsyncScope())
        {
            var services = scope.ServiceProvider;

            // Migrate UserDbContext
            await using var userContext = services.GetRequiredService<UserDbContext>();
            await userContext.Database.MigrateAsync();

            // Migrate ReviewDbContext
            await using var reviewContext = services.GetRequiredService<ReviewDbContext>();
            await reviewContext.Database.MigrateAsync();

            // Migrate EventDbContext
            await using var eventContext = services.GetRequiredService<EventDbContext>();
            await eventContext.Database.MigrateAsync();

            // Migrate CategoryDbContext
            await using var categoryContext = services.GetRequiredService<CategoryDbContext>();
            await categoryContext.Database.MigrateAsync();

            // Migrate BookingDbContext
            await using var bookingContext = services.GetRequiredService<BookingDbContext>();
            await bookingContext.Database.MigrateAsync();

            // Migrate AuthDbContext
            await using var authContext = services.GetRequiredService<AuthDbContext>();
            await authContext.Database.MigrateAsync();

            // Migrate FileDbContext
            await using var fileContext = services.GetRequiredService<FileDbContext>();
            await fileContext.Database.MigrateAsync();

            // Migrate PaymeDbContext
            await using var paymeContext = services.GetRequiredService<PaymeDbContext>();
            await paymeContext.Database.MigrateAsync();

            // Migrate TelegramDbContext
            await using var telegramContext = services.GetRequiredService<TelegramDbContext>();
            await telegramContext.Database.MigrateAsync();
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
        services.AddTransient<IBookingGroupRepository, BookingGroupRepository>();

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

        services.AddTransient<IFileDomainService, FileDomainService>();

        #endregion
    }

    public static void AddEventBus(this IServiceCollection services, IConfiguration configuration,
        Func<IBusRegistrationConfigurator, IBusRegistrationConfigurator>? config = null)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseInMemoryOutbox(context);

                cfg.Host(
                    configuration.GetSection("MessageBrokers:RabbitMQ:Host").Value,
                    configuration.GetValue<ushort>("MessageBrokers:RabbitMQ:Port"),
                    configuration.GetSection("MessageBrokers:RabbitMQ:VHost").Value,
                    h =>
                    {
                        h.Username(configuration.GetSection("MessageBrokers:RabbitMQ:Username").Value);
                        h.Password(configuration.GetSection("MessageBrokers:RabbitMQ:Password").Value);
                    });

                cfg.ConfigureEndpoints(context);
            });
            config?.Invoke(x);
        });
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