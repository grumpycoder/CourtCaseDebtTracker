using CaseTracker.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseTracker.Core.Repositories
{
    public interface ICaseRepository
    {
        Case GetByIdWithDetails(int id);
        void Add(Case @case);
        Case GetById(int id);
        void Remove(Case @case);
        int Count();
        IEnumerable<Case> GetAll();

        Task<Case> GetByIdWithDetailsAsync(int id);
        Task<Case> GetByIdAsync(int id);
        Task<IEnumerable<Case>> GetAllAsync();
        Task<int> CountAsync();
    }
}