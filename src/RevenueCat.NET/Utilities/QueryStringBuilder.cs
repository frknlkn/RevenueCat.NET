namespace RevenueCat.NET;

internal static class QueryStringBuilder
{
    public static string Build(int? limit = null, string? startingAfter = null, string? search = null, string? environment = null)
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

        if (!string.IsNullOrWhiteSpace(environment))
        {
            parameters.Add($"environment={Uri.EscapeDataString(environment)}");
        }

        return parameters.Count > 0 ? $"?{string.Join("&", parameters)}" : string.Empty;
    }

    public static string Build(Dictionary<string, string?> queryParams)
    {
        var parameters = new List<string>();

        foreach (var kvp in queryParams)
        {
            if (!string.IsNullOrWhiteSpace(kvp.Value))
            {
                parameters.Add($"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}");
            }
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

    public static string BuildWithBoolParam(string paramName, bool? paramValue)
    {
        if (!paramValue.HasValue)
        {
            return string.Empty;
        }

        return $"?{paramName}={paramValue.Value.ToString().ToLowerInvariant()}";
    }
}
