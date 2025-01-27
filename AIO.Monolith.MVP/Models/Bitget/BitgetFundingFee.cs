namespace GetFundingFees.Lambda.Models.Bitget;

public class BitgetFundingFee
{
    public required string Symbol { get; set; }
    public required string FundingRate { get; set; }
    public required string FundingTime { get; set; }
}