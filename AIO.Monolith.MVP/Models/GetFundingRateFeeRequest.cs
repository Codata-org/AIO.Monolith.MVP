namespace GetFundingFees.Lambda.Models;

public class GetFundingRateFeeRequest
{
    public required string Symbol { get; set; }
    public required Exchange Exchange { get; set; }
}

public enum Exchange
{
    Mexc,
    Bitget
}