using CaseTracker.Core;
using CaseTracker.Core.Repositories;
using CaseTracker.Data.Repositories;
using System.Threading.Tasks;

namespace CaseTracker.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ICourtRepository Courts { get; set; }
        public IJurisdictionRepository Jurisdictions { get; set; }
        public ICaseRepository Cases { get; set; }
        public ILitigantRepository Litigants { get; set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Courts = new CourtRepository(context);
            Jurisdictions = new JurisdictionRepository(context);
            Cases = new CaseRepository(context);
            Litigants = new LitigantRepository(context);
        }



        public void Complete()
        {
            _context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
