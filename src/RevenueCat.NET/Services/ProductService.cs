using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

internal sealed class ProductService(IHttpRequestExecutor executor) : IProductService
{
    public Task<ListResponse<Product>> ListAsync(
        string projectId,
        string? appId = null,
        int? limit = null,
        string? startingAfter = null,
        string[]? expand = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        
        var parameters = new List<string>();
        if (!string.IsNullOrWhiteSpace(appId))
        {
            parameters.Add($"app_id={Uri.EscapeDataString(appId)}");
        }
        if (limit.HasValue)
        {
            parameters.Add($"limit={limit.Value}");
        }
        if (!string.IsNullOrWhiteSpace(startingAfter))
        {
            parameters.Add($"starting_after={Uri.EscapeDataString(startingAfter)}");
        }
        if (expand is { Length: > 0 })
        {
            parameters.Add($"expand={string.Join(",", expand.Select(Uri.EscapeDataString))}");
        }

        var query = parameters.Count > 0 ? $"?{string.Join("&", parameters)}" : string.Empty;
        
        return executor.ExecuteAsync<ListResponse<Product>>(
            HttpMethod.Get,
            $"/projects/{projectId}/products{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Product> GetAsync(
        string projectId,
        string productId,
        string[]? expand = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(productId);
        
        var query = QueryStringBuilder.BuildExpand(expand);
        return executor.ExecuteAsync<Product>(
            HttpMethod.Get,
            $"/projects/{projectId}/products/{productId}{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Product> CreateAsync(
        string projectId,
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentNullException.ThrowIfNull(request);
        
        return executor.ExecuteAsync<Product>(
            HttpMethod.Post,
            $"/projects/{projectId}/products",
            request,
            cancellationToken);
    }

    public Task<DeletedObject> DeleteAsync(
        string projectId,
        string productId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(productId);
        
        return executor.ExecuteAsync<DeletedObject>(
            HttpMethod.Delete,
            $"/projects/{projectId}/products/{productId}",
            cancellationToken: cancellationToken);
    }
}
