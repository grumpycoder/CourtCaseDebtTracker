using System;
using System.Collections.Generic;

namespace CaseTracker.Core.Models
{
    public class Filing
    {
        public Filing()
        {
            Comments = new List<Comment>();
        }
        public int Id { get; set; }
        public string Plaintiff { get; set; }
        public string Defendant { get; set; }
        public DateTime? DateFiled { get; set; }
        public virtual Court Court { get; set; }
        public string CaseNumber { get; set; }
        public string Judge { get; set; }
        public string Summary { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CourtId { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public int FilingId { get; set; }
    }
}