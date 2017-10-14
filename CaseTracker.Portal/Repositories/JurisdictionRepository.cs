using CaseTracker.Core.Models;
using CaseTracker.Data;
using System.Collections.Generic;
using System.Linq;

namespace CaseTracker.Portal.Repositories
{
    public class JurisdictionRepository
    {
        private readonly AppDbContext context;

        public JurisdictionRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Jurisdiction> GetAll()
        {
            return context.Jurisdictions.ToList();
        }

        public int Count()
        {
            return context.Jurisdictions.Count();
        }

        public Jurisdiction GetById(int id)
        {
            return context.Jurisdictions.SingleOrDefault(j => j.Id == id);
        }

        public void Add(Jurisdiction jurisdiction)
        {
            context.Jurisdictions.Add(jurisdiction);
        }

        public void Remove(Jurisdiction jurisdiction)
        {
            context.Jurisdictions.Remove(jurisdiction);
        }
    }
}
