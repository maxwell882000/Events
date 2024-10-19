using EventsBookingBackend.Api.ControllerOptions.Conventions;
using EventsBookingBackend.Api.ControllerOptions.Filters;
using EventsBookingBackend.Api.ControllerOptions.Types;
using EventsBookingBackend.Api.Identity;
using EventsBookingBackend.Api.Options;
using EventsBookingBackend.Application.Common.Middlewares;
using EventsBookingBackend.Application.Services.Auth;
using EventsBookingBackend.Application.Services.Book;
using EventsBookingBackend.Application.Services.Category;
using EventsBookingBackend.Application.Services.Event;
using EventsBookingBackend.Application.Services.Review;
using EventsBookingBackend.Application.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace EventsBookingBackend.DependencyInjections;

public static class ApplicationDi
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IEventService, EventService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IReviewService, ReviewService>();
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IBookingTypeService, BookingTypeService>();
        services.AddTransient<ICategoryService, CategoryService>();
    }

    public static void UseMiddlewares(this IApplicationBuilder services)
    {
        services.UseMiddleware<ExceptionMiddleware>();
        services.UseMiddleware<LocalizationMiddleware>();
    }

    public static void AddAppOptions(this IServiceCollection services, IConfiguration configuration)
    {
        #region File

        services.Configure<HostOption>(
            configuration.GetSection(nameof(HostOption)));

        #endregion
    }

    public static void AddCommonExtensions(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwaggerProducesResponseTypesFilter>();
            c.DescribeAllParametersInCamelCase();
            // Define the BearerAuth scheme
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
            });
            // Require the BearerAuth scheme for the API
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressConsumesConstraintForFormFileParameters = true;
            options.SuppressInferBindingSourcesForParameters = true;
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SnakeCaseRoutingConvention()));
                options.Filters.Add<ValidationResponseFilter>();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                options.SerializerSettings.Converters.Add(new JsonClaimConverter());
                options.SerializerSettings.Converters.Add(new JsonClaimsPrincipalConverter());
                options.SerializerSettings.Converters.Add(new JsonClaimsIdentityConverter());
                options.SerializerSettings.Converters.Add(new PaymeMethodConverter());
                options.SerializerSettings.Converters.Add(new FileDtoConverter(services.BuildServiceProvider()
                    .GetRequiredService<IOptions<HostOption>>()));
            });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}