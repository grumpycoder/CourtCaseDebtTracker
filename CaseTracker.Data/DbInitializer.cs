using System.Linq;
using Newtonsoft.Json;
using CaseTracker.Core.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace CaseTracker.Data
{
    public static class DbInitializer
    {

        public static void Seed(IApplicationBuilder app, IHostingEnvironment env)
        {

            AppDbContext context = app.ApplicationServices.GetRequiredService<AppDbContext>();

            if (!context.Jurisdictions.Any())
            {
                var jurisdictions = JsonConvert.DeserializeObject<List<Jurisdiction>>(File.ReadAllText($@"{env.ContentRootPath}\DataFiles\jurisdiction-import.json"));
                context.Jurisdictions.AddRange(jurisdictions);
                context.SaveChanges();
            }

            if (!context.Courts.Any())
            {
                var courts = JsonConvert.DeserializeObject<List<CourtImport>>(File.ReadAllText($@"{env.ContentRootPath}\DataFiles\courts-import.json"));
                var jurisdictions = context.Jurisdictions.ToList();

                var joined = from court in courts
                             join jurisdiction in jurisdictions
                             on court.Jurisdiction.Trim() equals jurisdiction.Name.Trim() // join on some property
                             select new Court() { Name = court.Name, Abbreviation = court.Abbreviation, JurisdictionId = jurisdiction.Id };

                context.Courts.AddRange(joined);
                context.SaveChanges();
            }

            if (!context.Filings.Any())
            {
                var filings = JsonConvert.DeserializeObject<List<Filing>>(File.ReadAllText($@"{env.ContentRootPath}\DataFiles\filings-import.json"));
                context.Filings.AddRange(filings);
                context.SaveChanges();
            }
        }
    }
}