using System;

namespace SuperHeroCatalogue.Domain.Entities
{
    public class AuditEvent
    {   
        public int Id { get; set; }
        public string Entity { get; set; }
        public int EntityId { get; set; }
        public DateTime Datetime { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
    }
} 