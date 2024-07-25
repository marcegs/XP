using Application.Products;
using Application.Trades;
using Application.Trades.NewTrade;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Web.Entities;

namespace Web.Controllers.V1;

[Route("api/v1/[controller]")]
public class TradeController : BaseController<TradeController>
{
    public TradeController(ILogger<TradeController> logger) : base(logger)
    {
    }

    [HttpPost("buy")]
    public async Task<ActionResult<NewTradeResponse>> Buy(NewTradeBody body)
    {
        var request = new NewTradeRequest
        {
            TradeAmmount = body.TradeAmmount,
            ProductId = body.ProductId,
            TradeType = TradeType.Buy,
            AccountId = body.AccountId,
        };
        var result = await Mediator.Send(request);
        return HandleResult(result);
    }
    [HttpPost("sell")]
    public async Task<ActionResult<NewTradeResponse>> Sell(NewTradeBody body)
    {
        var request = new NewTradeRequest
        {
            TradeAmmount = body.TradeAmmount,
            ProductId = body.ProductId,
            TradeType = TradeType.Sell,
            AccountId = body.AccountId,
        };
        var result = await Mediator.Send(request);
        return HandleResult(result);
    }
}