using CaseTracker.Core.Models;
using CaseTracker.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CaseTracker.Data.Repositories
{
    public class JurisdictionRepository : IJurisdictionRepository
    {
        private readonly AppDbContext _context;

        public JurisdictionRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Jurisdiction> GetAll()
        {
            return Enumerable.ToList<Jurisdiction>(_context.Jurisdictions);
        }

        public int Count()
        {
            return Queryable.Count<Jurisdiction>(_context.Jurisdictions);
        }

        public Jurisdiction GetById(int id)
        {
            return Queryable.SingleOrDefault<Jurisdiction>(_context.Jurisdictions, j => j.Id == id);
        }

        public void Add(Jurisdiction jurisdiction)
        {
            _context.Jurisdictions.Add(jurisdiction);
        }

        public void Remove(Jurisdiction jurisdiction)
        {
            _context.Jurisdictions.Remove(jurisdiction);
        }
    }
}
