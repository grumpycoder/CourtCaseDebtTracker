using CaseTracker.Core.Models;
using System.Collections.Generic;

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
    }
}