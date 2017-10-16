using System.Collections.Generic;
using CaseTracker.Core.Models;

namespace CaseTracker.Core.Repositories
{
    public interface ICaseRepository
    {
        Filing GetByIdWithDetails(int id);
        void Add(Filing @case);
        Filing GetById(int id);
        void Remove(Filing @case);
        int Count();
        IEnumerable<Filing> GetAll();
    }
}