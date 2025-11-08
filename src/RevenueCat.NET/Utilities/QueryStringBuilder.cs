namespace RevenueCat.NET;

internal static class QueryStringBuilder
{
    public static string Build(int? limit = null, string? startingAfter = null, string? search = null)
    {
        var parameters = new List<string>();
        
        if (limit.HasValue)
        {
            parameters.Add($"limit={limit.Value}");
        }
        
        if (!string.IsNullOrWhiteSpace(startingAfter))
        {
            parameters.Add($"starting_after={Uri.EscapeDataString(startingAfter)}");
        }
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            parameters.Add($"search={Uri.EscapeDataString(search)}");
        }

        return parameters.Count > 0 ? $"?{string.Join("&", parameters)}" : string.Empty;
    }

    public static string BuildExpand(string[]? expand)
    {
        if (expand is null || expand.Length == 0)
        {
            return string.Empty;
        }

        var expandParam = string.Join(",", expand.Select(Uri.EscapeDataString));
        return $"?expand={expandParam}";
    }
}
