using System;

namespace Domain.Common
{
    public class AuditableEntity
    {
        public bool Deleted { get; set; } = false;
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}