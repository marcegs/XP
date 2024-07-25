using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Account: AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Trade> Trades { get; set; }
    }
}