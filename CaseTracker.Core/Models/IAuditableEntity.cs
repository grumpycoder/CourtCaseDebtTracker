using System;

namespace CaseTracker.Core.Models
{

    public interface IAuditableEntity
    {
        string CreatedUser { get; set; }
        string UpdatedUser { get; set; }
        DateTime? CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
    }
}
