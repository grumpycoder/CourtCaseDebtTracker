using CaseTracker.Core.Models;
using CaseTracker.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CaseTracker.Data.Repositories
{
    public class CaseRepository : ICaseRepository
    {
        private readonly AppDbContext _context;

        public CaseRepository(AppDbContext context)
        {
            _context = context;
        }


        public Filing GetByIdWithDetails(int id)
        {
            return EntityFrameworkQueryableExtensions.Include<Filing, Jurisdiction>(_context.Filings, f => f.Court.Jurisdiction).Include("Defendants").Include("Plaintiffs").SingleOrDefault(f => f.Id == id);
        }

        public void Add(Filing @case)
        {
            _context.Filings.Add(@case);
        }

        public Filing GetById(int id)
        {
            return Queryable.SingleOrDefault<Filing>(_context.Filings, c => c.Id == id);
        }

        public void Remove(Filing @case)
        {
            _context.Filings.Remove(@case);
        }

        public int Count()
        {
            return Queryable.Count<Filing>(_context.Filings);
        }

        public IEnumerable<Filing> GetAll()
        {
            return EntityFrameworkQueryableExtensions.Include<Filing, Court>(_context.Filings, c => c.Court).Include(c => c.Court.Jurisdiction).ToList();
        }


    }
}