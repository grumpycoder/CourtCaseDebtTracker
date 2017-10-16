using System.Collections.Generic;

namespace CaseTracker.Core.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FilingTag> Filings { get; set; }
    }

    public class FilingTag
    {
        public int FilingId { get; set; }
        public int TagId { get; set; }

        public Case Case { get; set; }
        public Tag Tag { get; set; }
    }
}
