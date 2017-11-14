using System;
using System.ComponentModel.DataAnnotations;

namespace SuperHeroCatalogue.Application.Models
{
    public class AuditEventViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Entity { get; set; }
        public int EntityId { get; set; }
        public DateTime Datetime { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
    }
}