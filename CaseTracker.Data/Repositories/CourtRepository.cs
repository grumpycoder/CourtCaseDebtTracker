using CaseTracker.Core.Models;
using CaseTracker.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CaseTracker.Data.Repositories
{
    public class CourtRepository : ICourtRepository
    {
        private readonly AppDbContext _context;

        public CourtRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Court> GetAll()
        {
            return EntityFrameworkQueryableExtensions.Include<Court>(_context.Courts, "Jurisdiction").Include("Filings").ToList();
        }

        public IEnumerable<Court> GetAll(Expression<Func<Court, bool>> predicate)
        {
            return EntityFrameworkQueryableExtensions.Include<Court>(_context.Courts, "Jurisdiction").ToList();
        }

        public Court GetById(int id)
        {
            return Queryable.SingleOrDefault<Court>(_context.Courts, c => c.Id == id);
        }

        public void Add(Court court)
        {
            _context.Courts.Add(court);
        }

        public Court GetByIdWithDetails(int id)
        {
            return EntityFrameworkQueryableExtensions.Include<Court, IEnumerable<Case>>(_context.Courts, c => c.Filings).SingleOrDefault(c => c.Id == id);
        }

        public int Count()
        {
            return Queryable.Count<Court>(_context.Courts);
        }

        public void Remove(Court court)
        {
            _context.Courts.Remove(court);
        }
    }
}