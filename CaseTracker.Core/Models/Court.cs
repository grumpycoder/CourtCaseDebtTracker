using System.Collections.Generic;

namespace CaseTracker.Core.Models
{
    public class Court
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public virtual Jurisdiction Jurisdiction { get; set; }
        public int JurisdictionId { get; set; }

        public virtual IEnumerable<Case> Filings { get; set; }
    }
}