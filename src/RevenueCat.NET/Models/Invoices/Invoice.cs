namespace RevenueCat.NET.Models;

public sealed record Invoice(
    string Object,
    string Id,
    string CustomerId,
    string SubscriptionId,
    long CreatedAt,
    InvoiceStatus Status,
    decimal Amount,
    string Currency
);

public enum InvoiceStatus
{
    Draft,
    Open,
    Paid,
    Void,
    Uncollectible
}
