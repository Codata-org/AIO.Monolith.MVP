namespace GetFundingFees.Lambda.Models.Mexc;

public class MexcFundingFee
{
    public required string Symbol { get; set; }
    public required decimal FundingRate { get; set; }
    public required long SettleTime { get; set; }
    public required int CollectCycle { get; set; }
}