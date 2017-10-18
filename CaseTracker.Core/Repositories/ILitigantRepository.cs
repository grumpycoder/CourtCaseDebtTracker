using CaseTracker.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseTracker.Core.Repositories
{
    public interface ILitigantRepository
    {
        void Add(Litigant litigant);
        void AddDefendant(Defendant defendant);
        void AddPlaintiff(Plaintiff plaintiff);
        int Count();
        IEnumerable<Litigant> GetAll();
        Litigant GetById(int id);
        void Remove(Litigant litigant);

        Task<Litigant> GetByIdAsync(int id);
        Task<IEnumerable<Litigant>> GetAllAsync();
        Task<int> CountAsync();
    }
}