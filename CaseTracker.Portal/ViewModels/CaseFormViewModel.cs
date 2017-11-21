using System;

namespace CaseTracker.Portal.ViewModels
{
    public class CaseFormViewModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Judge { get; set; }
        public string Summary { get; set; }
        public int CourtId { get; set; }
        public DateTime? DateFiled { get; set; }
    }
}