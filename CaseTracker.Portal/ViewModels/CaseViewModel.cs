using System;

namespace CaseTracker.Portal.ViewModels
{
    public class CaseViewModel
    {
        public int Id { get; set; }
        public string CaseNumber { get; set; }
        public string Caption { get; set; }
        public string Judge { get; set; }
        public string Summary { get; set; }
        public int CourtId { get; set; }
        public DateTime? DateFiled { get; internal set; }
    }
}