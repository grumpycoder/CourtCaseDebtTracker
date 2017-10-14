using CaseTracker.Data;
using CaseTracker.Portal.Repositories;

namespace CaseTracker.Portal.Persistence
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;
        public CourtRepository Courts { get; set; }
        public JurisdictionRepository Jurisdictions { get; set; }
        public CaseRepository Cases { get; set; }
        public LitigantRepository Litigants { get; set; }

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

    }
}
