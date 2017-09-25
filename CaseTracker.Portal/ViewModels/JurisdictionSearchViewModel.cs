namespace CaseTracker.Portal.ViewModels
{
    public class JurisdictionSearchViewModel : PagerModel<JurisdictionViewModel>
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Jurisdiction { get; set; }
    }
}