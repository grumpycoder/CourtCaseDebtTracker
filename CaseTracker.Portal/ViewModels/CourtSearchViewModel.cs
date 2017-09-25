namespace CaseTracker.Portal.ViewModels
{
    public class CourtSearchViewModel : PagerModel<CourtViewModel>
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Jurisdiction { get; set; }


    }
}