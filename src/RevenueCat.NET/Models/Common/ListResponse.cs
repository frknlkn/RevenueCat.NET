namespace RevenueCat.NET.Models;

public sealed record ListResponse<T>(
    string Object,
    IReadOnlyList<T> Items,
    string Url,
    string? NextPage = null
);
