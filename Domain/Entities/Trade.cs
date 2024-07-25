using Domain.Common;
using Domain.Entities;

namespace Domain.Entities
{
    public class Trade : AuditableEntity
    {
        public int Id { get; set; }
        public TradeType TradeType { get; set; }
        public double TradeAmmount { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}