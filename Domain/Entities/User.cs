using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime Birthdate { get; set; }

        public ICollection<Account> Accounts { get; set; }
        
    }
}