using CaseTracker.Core.Repositories;
using System.Threading.Tasks;

namespace CaseTracker.Core
{
    public interface IUnitOfWork
    {
        ICourtRepository Courts { get; set; }
        IJurisdictionRepository Jurisdictions { get; set; }
        ICaseRepository Cases { get; set; }
        ILitigantRepository Litigants { get; set; }
        void Complete();
        Task CompleteAsync();
    }
}