using System.Net.Http.Json;
using GetFundingFees.Lambda.Models;
using GetFundingFees.Lambda.Models.Bitget;

namespace GetFundingFees.Lambda.ApiClients;

public class BitgetClient : IGetFundingFeeClient
{
    public async Task<List<FundingFee>> GetFundingFeesAsync(string symbol)
    {
        HttpClient client = new();
        
        var response = await client.GetAsync($"https://api.bitget.com/api/v2/mix/market/history-fund-rate?symbol={symbol}&productType=usdt-futures&pageSize=100");

        var content = await response.Content.ReadFromJsonAsync<BitgetResponse<List<BitgetFundingFee>>>();
        
        var fundingFees = content?.Data?.Select(x => new FundingFee
        {
            FundingRate = decimal.Parse(x.FundingRate),
            SettleDate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(x.FundingTime)).UtcDateTime,
            Exchange = Exchange.Bitget
        }).ToList();

        // Calculate the cycle in hours
        for (int i = 1; i < fundingFees.Count; i++)
        {
            var previous = fundingFees[i - 1];
            var current = fundingFees[i];

            // Calculate time difference in hours
            var difference = (previous.SettleDate - current.SettleDate).TotalHours;
            current.CollectCycle = (int)difference;
        }
        
        return fundingFees;
    }
}