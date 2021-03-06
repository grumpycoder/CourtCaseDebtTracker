namespace CaseTracker.Portal.ViewModels
{
    public class CourtViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int JurisdictionId { get; set; }
        public string Jurisdiction { get; set; }
        public int? NumberCases { get; set; }
    }
}