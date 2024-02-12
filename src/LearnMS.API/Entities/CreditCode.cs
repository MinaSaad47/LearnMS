using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LearnMS.API.Entities;

public sealed class CreditCode
{
    public required string Code { get; set; }
    public decimal Value { get; set; }
    public Guid? GeneratorId { get; set; }
    private Guid? _sellerId;
    public Guid? SellerId
    {
        get => _sellerId;
        set
        {
            Status = CreditCodeStatus.Sold;
            SoldAt = DateTime.UtcNow;
            _sellerId = value;
        }
    }
    private Guid? _studentId;
    public Guid? StudentId
    {
        get => _studentId; set
        {
            Status = CreditCodeStatus.Redeemed;
            RedeemedAt = DateTime.UtcNow;
            _studentId = value;
        }
    }
    public CreditCodeStatus Status { get; private set; } = CreditCodeStatus.Fresh;
    public DateTime GeneratedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? SoldAt { get; private set; }
    public DateTime? RedeemedAt { get; private set; }

}

[JsonConverter(typeof(StringEnumConverter))]
public enum CreditCodeStatus
{
    Fresh,
    Sold,
    Redeemed
}