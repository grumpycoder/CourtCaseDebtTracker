using System.Collections.Generic;
using CaseTracker.Core.Models;

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
    }
}