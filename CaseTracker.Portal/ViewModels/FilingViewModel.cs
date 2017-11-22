using CaseTracker.Core.Models;
using CaseTracker.Portal.Helpers;
using System;
using System.Collections.Generic;

namespace CaseTracker.Portal.ViewModels
{
    public class CaseDetailViewModel
    {

        public int Id { get; set; }
        public string Caption { get; set; }
        public List<Plaintiff> Plaintiffs { get; set; }
        public List<Defendant> Defendants { get; set; }
        public int? Date { get; set; }
        public virtual Court Court { get; set; }
        public string Jurisdiction { get; set; }
        public string Judge { get; set; }
        public string Summary { get; set; }
        public int CourtId { get; set; }
        public string CourtName { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public List<string> Tags { get; set; }

        public bool CanDelete { get; set; }
        public bool CanEdit { get; set; }

        public bool HasBeenEdited
        {
            get { return CreateDate?.TrimSeconds() != UpdateDate?.TrimSeconds(); }
        }
    }
}
