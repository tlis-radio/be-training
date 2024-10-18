using System.Diagnostics.CodeAnalysis;

namespace Core.Domain.Model;

public class BusynessRuleException(string message) : Exception(message)
{
    public static T AccessingUninitialisedState<T>() =>
        throw new BusynessRuleException("Attempt to access uninitialised state");
    
    public static void ThrowIf(Func<bool> predicate, string message = "", params object[] format)
    {
        if (predicate())
            throw new BusynessRuleException(string.Format(message, format));
    }

    public static void ThrowIfNullOrWhiteSpace([NotNull] string? value, string message = "") =>
        ThrowIf(() => string.IsNullOrWhiteSpace(value), message);

    public static void ThrowIfNull([NotNull] object? value, string message = "") =>
        ThrowIf(() => value is null, message);

    public static void ThrowIfShorterThan(string value, int minLength, string message = "") =>
        ThrowIf(() => value.Length < minLength, message, value, minLength);
    
    public static void ThrowIfLongerThan(string value, int maxLength, string message = "") =>
        ThrowIf(() => value.Length > maxLength, message, value, maxLength);
}
