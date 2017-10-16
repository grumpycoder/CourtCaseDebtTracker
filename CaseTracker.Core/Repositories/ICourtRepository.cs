using CaseTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CaseTracker.Core.Repositories
{
    public interface ICourtRepository
    {
        void Add(Court court);
        IEnumerable<Court> GetAll();
        IEnumerable<Court> GetAll(Expression<Func<Court, bool>> predicate);
        int Count();
        Court GetById(int id);
        Court GetByIdWithDetails(int id);
        void Remove(Court court);
    }
}