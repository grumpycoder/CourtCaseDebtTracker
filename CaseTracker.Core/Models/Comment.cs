namespace CaseTracker.Core.Models
{
    public class Comment : AuditableEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        // public virtual Case Case { get; set; }
        public int FilingId { get; set; }
    }
}