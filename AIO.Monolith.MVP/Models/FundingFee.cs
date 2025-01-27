namespace GetFundingFees.Lambda.Models;

public class FundingFee
{
    public required DateTime SettleDate { get; set; }
    public required decimal FundingRate { get; set; }
    public int? CollectCycle { get; set; } 
    public Exchange Exchange { get; set; }
}