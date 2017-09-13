using System;

namespace CaseTracker.Core.Models
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}