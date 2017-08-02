using System;

namespace CaseTracker.Core.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreateDate { get; set; }
        // public virtual Filing Filing { get; set; }
        public int FilingId { get; set; }
    }
}