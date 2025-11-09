using System.Text.Json;
using System.Text.Json.Serialization;

namespace RevenueCat.NET.Serialization;

/// <summary>
/// JSON converter for enums that uses snake_case naming.
/// </summary>
internal sealed class SnakeCaseEnumConverter<TEnum> : JsonStringEnumConverter<TEnum>
    where TEnum : struct, Enum
{
    public SnakeCaseEnumConverter() : base(JsonNamingPolicy.SnakeCaseLower, allowIntegerValues: false)
    {
    }
}
