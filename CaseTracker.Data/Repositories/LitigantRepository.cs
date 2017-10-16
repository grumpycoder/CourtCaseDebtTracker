using CaseTracker.Core.Models;
using CaseTracker.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CaseTracker.Data.Repositories
{
    public class LitigantRepository : ILitigantRepository
    {
        private readonly AppDbContext _context;

        public LitigantRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Litigant> GetAll()
        {
            return Enumerable.ToList<Litigant>(_context.Litigants);
        }

        public int Count()
        {
            return Queryable.Count<Jurisdiction>(_context.Jurisdictions);
        }

        public Litigant GetById(int id)
        {
            return Queryable.SingleOrDefault<Litigant>(_context.Litigants, j => j.Id == id);
        }

        public void Add(Litigant litigant)
        {
            _context.Litigants.Add(litigant);
        }

        public void Remove(Litigant litigant)
        {
            _context.Litigants.Remove(litigant);
        }

        public void AddDefendant(Defendant defendant)
        {
            _context.Defendants.Add(defendant);
        }

        public void AddPlaintiff(Plaintiff plaintiff)
        {
            _context.Plaintiffs.Add(plaintiff);
        }
    }
}
