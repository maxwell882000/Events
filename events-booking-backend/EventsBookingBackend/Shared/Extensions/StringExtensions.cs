namespace EventsBookingBackend.Shared.Extensions;

public static class StringExtensions
{
    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str) || !char.IsUpper(str[0]))
            return str;

        var camelCase = char.ToLower(str[0]) + str.Substring(1);
        return camelCase;
    }
}