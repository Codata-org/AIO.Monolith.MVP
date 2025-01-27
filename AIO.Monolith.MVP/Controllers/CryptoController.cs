using System.Text.Json;
using GetFundingFees.Lambda.ApiClients;
using GetFundingFees.Lambda.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIO.Monolith.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CryptoController : Controller
{
    [HttpPost]
    [Route("GetFundingRateFee")]
    public async Task<ActionResult<string>> GetFundingRateFee([FromServices] BitgetClient bitgetClient,
        [FromServices] MexcClient mexcClient,
        [FromBody] List<GetFundingRateFeeRequest> request)
    {
        var response = new List<FundingFee>();
        
        foreach (var feeRequest in request)
        {
            var fundingFees = feeRequest.Exchange switch
            {
                Exchange.Bitget => await bitgetClient.GetFundingFeesAsync(feeRequest.Symbol),
                Exchange.Mexc => await mexcClient.GetFundingFeesAsync(feeRequest.Symbol)
            };
            
            response.AddRange(fundingFees);
        }

        return Ok(response.OrderByDescending(d => d.SettleDate));
    }
}