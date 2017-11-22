using System;

namespace CaseTracker.Portal.ViewModels
{
    public class CaseViewModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Judge { get; set; }
        public string Summary { get; set; }
        public string Court { get; set; }
        public string Jurisdiction { get; set; }
        public int CourtId { get; set; }
        public int? Date { get; set; }
    }
}