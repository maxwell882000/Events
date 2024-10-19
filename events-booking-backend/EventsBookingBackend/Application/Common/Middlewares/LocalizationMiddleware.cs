using System.Globalization;

namespace EventsBookingBackend.Application.Common.Middlewares;

public class LocalizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // Set the culture key based on the request header
        var cultureKey = context.Request.Headers["Accept-Language"];

        if (string.IsNullOrEmpty(cultureKey))
        {
            cultureKey = "ru-RU";
        }


        // Check if the culture exists
        if (DoesCultureExist(cultureKey))
        {
            // Set the culture Info
            var culture = new CultureInfo(cultureKey);

            // Set the culture in the current thread responsible for that request
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }


        // Await the next request
        await next(context);
    }

    private static bool DoesCultureExist(string cultureName)
    {
        // Return the culture where the culture equals the culture name set
        return CultureInfo.GetCultures(CultureTypes.AllCultures).Any(culture => string.Equals(culture.Name, cultureName,
            StringComparison.CurrentCultureIgnoreCase));
    }
}