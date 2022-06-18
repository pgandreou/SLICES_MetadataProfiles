namespace Slices.Common;

public static class EnumerableExtensions
{
    public static bool ContainsAny<T>(this IEnumerable<T> items, params T[] candidates)
        => items.Any(candidates.Contains);
}