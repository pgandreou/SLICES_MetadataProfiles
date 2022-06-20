namespace Slices.Common;

public static class EnumerableExtensions
{
    public static bool ContainsAny<T>(this IEnumerable<T> items, params T[] candidates)
        => items.Any(candidates.Contains);

    /// <summary>
    /// Applies a transformation to the entire sequence. Useful for inserting generators in the
    /// middle of a LINQ chain 
    /// </summary>
    public static IEnumerable<TResult> SequenceSelect<TSource, TResult>(
        this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> selector
    )
        => selector(source);
}