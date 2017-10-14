using CaseTracker.Core.Models;
using CaseTracker.Data;
using System.Collections.Generic;
using System.Linq;

namespace CaseTracker.Portal.Repositories
{
    public class LitigantRepository
    {
        private readonly AppDbContext context;

        public LitigantRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Litigant> GetAll()
        {
            return context.Litigants.ToList();
        }

        public int Count()
        {
            return context.Jurisdictions.Count();
        }

        public Litigant GetById(int id)
        {
            return context.Litigants.SingleOrDefault(j => j.Id == id);
        }

        public void Add(Litigant litigant)
        {
            context.Litigants.Add(litigant);
        }

        public void Remove(Litigant litigant)
        {
            context.Litigants.Remove(litigant);
        }

        public void AddDefendant(Defendant defendant)
        {
            context.Defendants.Add(defendant);
        }

        public void AddPlaintiff(Plaintiff plaintiff)
        {
            context.Plaintiffs.Add(plaintiff);
        }
    }
}
