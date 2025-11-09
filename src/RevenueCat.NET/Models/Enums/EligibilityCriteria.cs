using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using RevenueCat.NET.Serialization;

namespace RevenueCat.NET.Models.Enums;

/// <summary>
/// Eligibility criteria for product availability.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter<EligibilityCriteria>))]
public enum EligibilityCriteria
{
    /// <summary>
    /// Available to all customers.
    /// </summary>
    [EnumMember(Value = "all")]
    All,
    
    /// <summary>
    /// Available only to customers using Google SDK version less than 6.
    /// </summary>
    [EnumMember(Value = "google_sdk_lt_6")]
    GoogleSdkLessThan6,
    
    /// <summary>
    /// Available only to customers using Google SDK version 6 or greater.
    /// </summary>
    [EnumMember(Value = "google_sdk_ge_6")]
    GoogleSdkGreaterOrEqual6
}
