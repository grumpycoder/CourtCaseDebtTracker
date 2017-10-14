using CaseTracker.Core.Models;
using CaseTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CaseTracker.Portal.Repositories
{
    public class CaseRepository
    {
        private readonly AppDbContext context;

        public CaseRepository(AppDbContext context)
        {
            this.context = context;
        }


        public Filing GetByIdWithDetails(int id)
        {
            return context.Filings.Include(f => f.Court.Jurisdiction).Include("Defendants").Include("Plaintiffs").SingleOrDefault(f => f.Id == id);
        }

        public void Add(Filing @case)
        {
            context.Filings.Add(@case);
        }

        public Filing GetById(int id)
        {
            return context.Filings.SingleOrDefault(c => c.Id == id);
        }

        public void Remove(Filing @case)
        {
            context.Filings.Remove(@case);
        }

        public int Count()
        {
            return context.Filings.Count();
        }

        public IEnumerable<Filing> GetAll()
        {
            return context.Filings.Include(c => c.Court).Include(c => c.Court.Jurisdiction).ToList();
        }


    }
}