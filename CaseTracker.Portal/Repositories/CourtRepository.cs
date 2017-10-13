using System;
using System.Collections.Generic;
using System.Linq;
using CaseTracker.Data; 
using CaseTracker.Core.Models; 

namespace CaseTracker.Portal.Repositories
{
    public class CourtRepository
    {
        private readonly AppDbContext context;

        public CourtRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<Court> GetAll(){
            return context.Courts.ToList();
        }

        public Court GetById(int id){
            return context.Courts.SingleOrDefault(c => c.Id == id);
        }
    }
}