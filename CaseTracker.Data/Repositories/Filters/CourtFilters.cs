using CaseTracker.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace CaseTracker.Data.Repositories
{
    public static class CourtFilters
    {
        public static IEnumerable<Court> WithNameLike(this IEnumerable<Court> courtsQuery,
                                               string title)
        {
            if (!string.IsNullOrEmpty(title))
                courtsQuery = courtsQuery.Where(p => p.Name.ToLower().Contains(title.ToLower()));

            return courtsQuery;
        }

        public static IEnumerable<Court> WithJurisdictionLike(this IEnumerable<Court> courtsQuery,
            string jurisdiction)
        {
            if (!string.IsNullOrEmpty(jurisdiction))
                courtsQuery = courtsQuery.Where(p => p.Jurisdiction.Name.ToLower().Contains(jurisdiction.ToLower()));

            return courtsQuery;
        }

        public static IEnumerable<Court> WithJurisdictionId(this IEnumerable<Court> courtsQuery,
            int? id)
        {
            if (id != null)
                courtsQuery = courtsQuery.Where(p => p.JurisdictionId == id);

            return courtsQuery;
        }

        public static IEnumerable<Court> WithAbbreviationLike(this IEnumerable<Court> courtsQuery,
            string abbreviation)
        {
            if (!string.IsNullOrEmpty(abbreviation))
                courtsQuery = courtsQuery.Where(p => p.Abbreviation.ToLower().Contains(abbreviation.ToLower()));

            return courtsQuery;
        }


        public static IEnumerable<Court> WithPaging(this IEnumerable<Court> courtsQuery,
                                            int? startRow,
                                            int? rowCount)
        {
            if ((!startRow.HasValue) && (!rowCount.HasValue || rowCount.Value == 0))
                return courtsQuery;

            return courtsQuery.Skip((int)startRow).Take((int)rowCount);
        }

    }
}
