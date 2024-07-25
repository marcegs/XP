using Domain.Common;
using FluentResults;
using MediatR;

namespace Application.Trades.NewTrade;

public class NewTradeRequest : IRequest<Result<NewTradeResponse>>
{
    public int AccountId { get; set; }
    public int ProductId { get; set; }
    public TradeType TradeType { get; set; }
    public double TradeAmmount { get; set; }
}