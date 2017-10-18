using CaseTracker.Core.Models;
using CaseTracker.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return _context.Litigants.ToList();
        }

        public async Task<IEnumerable<Litigant>> GetAllAsync()
        {
            return await _context.Litigants.ToListAsync();
        }

        public int Count()
        {
            return _context.Litigants.Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Litigants.CountAsync();
        }

        public Litigant GetById(int id)
        {
            return _context.Litigants.SingleOrDefault(l => l.Id == id);
        }

        public async Task<Litigant> GetByIdAsync(int id)
        {
            return await _context.Litigants.SingleOrDefaultAsync(l => l.Id == id);
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
