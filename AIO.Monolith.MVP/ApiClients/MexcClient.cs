using System.Net.Http.Json;
using App1.Models;
using App1.Models.Mexc;
using GetFundingFees.Lambda.Models;
using GetFundingFees.Lambda.Models.Mexc;

namespace GetFundingFees.Lambda.ApiClients;

public class MexcClient : IGetFundingFeeClient
{
    public async Task<List<FundingFee>> GetFundingFeesAsync(string symbol)
    {
        HttpClient client = new();
        
        var response = await client.GetAsync($"https://futures.mexc.com/api/v1/contract/funding_rate/historyOfDay?day=14&symbol={symbol}");

        var content = await response.Content.ReadFromJsonAsync<MexcResponse<List<MexcFundingFee>>>();
        
        var fundingFees = content?.Data?.Select(x => new FundingFee
        {
            FundingRate = x.FundingRate,
            SettleDate = DateTimeOffset.FromUnixTimeMilliseconds(x.SettleTime).UtcDateTime,
            CollectCycle = x.CollectCycle,
            Exchange = Exchange.Mexc
        }).ToList();

        return fundingFees;
    }
}