using Application.Common.DTOs;

namespace Application.Accounts.GetAccountTrades;

public class GetAccountTradesResponse
{
    public int count { get; set; }
    public double sum { get; set; }
    public List<TradeDTO> Trades { get; set; }
}