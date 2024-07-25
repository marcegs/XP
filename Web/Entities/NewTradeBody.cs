namespace Web.Entities;

public class NewTradeBody
{
    public int AccountId { get; set; }
    public int ProductId { get; set; }
    public double TradeAmmount { get; set; }
}