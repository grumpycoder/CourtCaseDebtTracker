using System.Collections.Generic;
using CaseTracker.Core.Models;

namespace CaseTracker.Core.Repositories
{
    public interface IJurisdictionRepository
    {
        void Add(Jurisdiction jurisdiction);
        int Count();
        IEnumerable<Jurisdiction> GetAll();
        Jurisdiction GetById(int id);
        void Remove(Jurisdiction jurisdiction);
    }
}