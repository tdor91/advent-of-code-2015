﻿namespace Common;

public static class EnumerableExtensions
{
    public static IEnumerable<(T A, T B)> Pairwise<T>(this IEnumerable<T> source)
    {
        return source.Zip(source.Skip(1));
    }

    public static IEnumerable<(T A, T B, T C)> Tripples<T>(this IEnumerable<T> source)
    {
        for (int i = 0; i < source.Count() - 2; i++)
        {
            yield return (source.ElementAt(i), source.ElementAt(i + 1), source.ElementAt(i + 2));
        }
    }

    public static IEnumerable<int> ToInts(this IEnumerable<string> source) => source.Select(int.Parse);

    public static IEnumerable<T> WithoutAt<T>(this IEnumerable<T> source, int index)
    {
        int i = 0;
        foreach (var item in source)
        {
            if (i != index)
            {
                yield return item;
            }
            i++;
        }
    }

    public static string AsString(this IEnumerable<string> source, string separator = ",") => string.Join(separator, source);
}