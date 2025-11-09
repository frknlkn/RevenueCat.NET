using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Customers;

/// <summary>
/// Represents a customer's enrollment in an experiment.
/// </summary>
public class ExperimentEnrollment
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "experiment_enrollment";
    
    /// <summary>
    /// The experiment ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// The variant the customer is enrolled in.
    /// </summary>
    [JsonPropertyName("variant")]
    public string Variant { get; set; } = string.Empty;
    
    /// <summary>
    /// The date when the customer was enrolled in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("enrolled_at")]
    public long EnrolledAt { get; set; }
}
