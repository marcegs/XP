using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        
        public IEnumerable<Trade> Trades { get; set; }
    }
}