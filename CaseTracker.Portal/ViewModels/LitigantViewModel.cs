using CaseTracker.Core.Models;

namespace CaseTracker.Portal.ViewModels
{
    public class LitigantViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaseId { get; set; }
        public LitigantType Type { get; set; }
    }
}