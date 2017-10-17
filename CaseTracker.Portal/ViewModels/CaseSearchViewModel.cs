using System;
using System.Collections.Generic;

namespace CaseTracker.Portal.ViewModels
{
    public class CaseSearchViewModel : PagerModel<CaseViewModel>
    {
        public string Caption { get; set; }
        public DateTime? DateFiled { get; set; }
        public string Court { get; set; }
        public string CaseNumber { get; set; }
        public string Judge { get; set; }
        public string Jurisdiction { get; set; }
    }

    public class PagerModel<T>
    {
        public int? Page { get; set; } = 0;
        public int? PageSize { get; set; } = 20;
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }

        public double TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public IEnumerable<T> Results { get; set; }
    }

}