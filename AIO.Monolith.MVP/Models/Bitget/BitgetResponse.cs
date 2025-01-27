namespace GetFundingFees.Lambda.Models.Bitget;

public class BitgetResponse<T>
{
    public required string Code { get; set; }
    public required string Msg { get; set; }
    public required long RequestTime { get; set; }
    public required T Data { get; set; }
}