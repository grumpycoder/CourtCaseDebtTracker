using CaseTracker.Core.Repositories;

namespace CaseTracker.Core
{
    public interface IUnitOfWork
    {
        ICourtRepository Courts { get; set; }
        IJurisdictionRepository Jurisdictions { get; set; }
        ICaseRepository Cases { get; set; }
        ILitigantRepository Litigants { get; set; }
        void Complete();
    }
}