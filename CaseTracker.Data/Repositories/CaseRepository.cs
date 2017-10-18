using CaseTracker.Core.Models;
using CaseTracker.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return _context.Cases.OrderByDescending(c => c.Id).Include(c => c.Court.Jurisdiction).Include(c => c.Defendants).Include(c => c.Plaintiffs).SingleOrDefault(f => f.Id == id);
        }

        public async Task<Case> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Cases.OrderByDescending(c => c.Id).Include(c => c.Court.Jurisdiction).Include(c => c.Defendants).Include(c => c.Plaintiffs).SingleOrDefaultAsync(f => f.Id == id);
        }

        public void Add(Case @case)
        {
            _context.Cases.Add(@case);
        }

        public Case GetById(int id)
        {
            return _context.Cases.SingleOrDefault(c => c.Id == id);
        }

        public async Task<Case> GetByIdAsync(int id)
        {
            return await _context.Cases.SingleOrDefaultAsync(c => c.Id == id);
        }

        public void Remove(Case @case)
        {
            _context.Cases.Remove(@case);
        }

        public int Count()
        {
            return _context.Cases.Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Cases.CountAsync();
        }

        public IEnumerable<Case> GetAll()
        {
            return _context.Cases.Include(c => c.Court).Include(c => c.Court.Jurisdiction).ToList();
        }

        public async Task<IEnumerable<Case>> GetAllAsync()
        {
            return await _context.Cases.Include(c => c.Court).Include(c => c.Court.Jurisdiction).ToListAsync();
        }

    }
}