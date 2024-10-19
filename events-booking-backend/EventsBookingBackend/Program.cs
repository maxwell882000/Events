using System.Globalization;
using EventsBookingBackend.DependencyInjections;
using EventsBookingBackend.Infrastructure.Payment.Payme.DI;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuth();
builder.Services.AddDatabases(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddCommonExtensions();
builder.Services.AddServices();
builder.Services.AddAppOptions(builder.Configuration);
builder.Services.AddInfraOptions(builder.Configuration);
builder.Services.AddPayme();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowEverything",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("ru-Ru") };
    options.DefaultRequestCulture = new RequestCulture("ru-Ru");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
var app = builder.Build();

app.UseCors("AllowEverything");

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI(c => c.DisplayRequestDuration());
// }

// Configure the HTTP request pipeline.
var supportedCultures = new[] { new CultureInfo("ru-RU") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru-RU"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});


app.UseStaticFiles();
app.UseMiddlewares();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

await app.UseMigration();

app.Run();