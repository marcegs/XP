namespace Application.Common.DTOs;

public class TradeDTO
{
    public int Id { get; set; }
    public double TradeAmmount { get; set; }
    public string TradeType { get; set; }
    public AccountDTO Account { get; set; }
    public ProductDTO Product { get; set; }
}