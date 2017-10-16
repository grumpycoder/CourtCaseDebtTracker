using CaseTracker.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace CaseTracker.Data.Repositories
{
    public static class JurisdictionFilters
    {
        public static IEnumerable<Jurisdiction> WithNameLike(this IEnumerable<Jurisdiction> jurisdictionQuery,
                                               string title)
        {
            if (!string.IsNullOrEmpty(title))
                jurisdictionQuery = jurisdictionQuery.Where(p => p.Name.ToLower().Contains(title.ToLower()));

            return jurisdictionQuery;
        }

        public static IEnumerable<Jurisdiction> WithAbbreviationLike(this IEnumerable<Jurisdiction> jurisdictionQuery,
            string abbreviation)
        {
            if (!string.IsNullOrEmpty(abbreviation))
                jurisdictionQuery = jurisdictionQuery.Where(p => p.Abbreviation.ToLower().Contains(abbreviation.ToLower()));

            return jurisdictionQuery;
        }


        public static IEnumerable<Jurisdiction> WithPaging(this IEnumerable<Jurisdiction> jurisdictionQuery,
                                            int? startRow,
                                            int? rowCount)
        {
            if ((!startRow.HasValue) && (!rowCount.HasValue || rowCount.Value == 0))
                return jurisdictionQuery;

            return jurisdictionQuery.Skip((int)startRow).Take((int)rowCount);
        }

    }
}
