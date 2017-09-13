namespace CaseTracker.Core.Models
{
    public abstract class Litigant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FilingId { get; set; }
    }
}