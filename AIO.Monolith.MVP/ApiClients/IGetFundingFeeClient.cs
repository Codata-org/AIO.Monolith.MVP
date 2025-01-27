using GetFundingFees.Lambda.Models;

namespace GetFundingFees.Lambda.ApiClients;

public interface IGetFundingFeeClient
{
    Task<List<FundingFee>> GetFundingFeesAsync(string symbol);
}