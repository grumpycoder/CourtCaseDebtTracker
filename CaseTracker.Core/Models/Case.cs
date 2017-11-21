using System;
using System.Collections.Generic;

namespace CaseTracker.Core.Models
{
    public class Case : AuditableEntity
    {
        public Case()
        {
            Comments = new List<Comment>();
        }
        public int Id { get; set; }
        public string Caption { get; set; }
        public ICollection<Plaintiff> Plaintiffs { get; set; }
        public ICollection<Defendant> Defendants { get; set; }
        public DateTime? DateFiled { get; set; }
        public virtual Court Court { get; set; }
        public string Judge { get; set; }
        public string Summary { get; set; }
        public int CourtId { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public virtual ICollection<FilingTag> Tags { get; set; }

        public int FilingId { get; set; }
    }
}