using CaseTracker.Core.Models;
using CaseTracker.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CaseTracker.Portal.Repositories
{
    public class CourtRepository
    {
        private readonly AppDbContext context;

        public CourtRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Court> GetAll()
        {
            return context.Courts.Include("Jurisdiction").Include("Filings").ToList();
        }

        public IEnumerable<Court> GetAll(Expression<Func<Court, bool>> predicate)
        {
            return context.Courts.Include("Jurisdiction").ToList();
        }

        public Court GetById(int id)
        {
            return context.Courts.SingleOrDefault(c => c.Id == id);
        }

        public void Add(Court court)
        {
            context.Courts.Add(court);
        }

        public Court GetByIdWithDetails(int id)
        {
            return context.Courts.Include(c => c.Filings).SingleOrDefault(c => c.Id == id);
        }

        internal int Count()
        {
            return context.Courts.Count();
        }

        public void Remove(Court court)
        {
            context.Courts.Remove(court);
        }
    }
}