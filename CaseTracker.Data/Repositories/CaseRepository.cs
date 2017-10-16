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


        public Case GetByIdWithDetails(int id)
        {
            return EntityFrameworkQueryableExtensions.Include<Case, Jurisdiction>(_context.Cases, f => f.Court.Jurisdiction).Include("Defendants").Include("Plaintiffs").SingleOrDefault(f => f.Id == id);
        }

        public void Add(Case @case)
        {
            _context.Cases.Add(@case);
        }

        public Case GetById(int id)
        {
            return Queryable.SingleOrDefault<Case>(_context.Cases, c => c.Id == id);
        }

        public void Remove(Case @case)
        {
            _context.Cases.Remove(@case);
        }

        public int Count()
        {
            return Queryable.Count<Case>(_context.Cases);
        }

        public IEnumerable<Case> GetAll()
        {
            return EntityFrameworkQueryableExtensions.Include<Case, Court>(_context.Cases, c => c.Court).Include(c => c.Court.Jurisdiction).ToList();
        }


    }
}