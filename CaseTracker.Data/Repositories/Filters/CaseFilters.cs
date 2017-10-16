using CaseTracker.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace CaseTracker.Data.Repositories
{
    public static class CaseFilters
    {
        public static IEnumerable<Case> WithCaptionLike(this IEnumerable<Case> filingQuery,
                                               string caption)
        {
            if (!string.IsNullOrEmpty(caption))
                filingQuery = filingQuery.Where(p => p.Caption.ToLower().Contains(caption.ToLower()));

            return filingQuery;
        }

        public static IEnumerable<Case> WithCaseNumberLike(this IEnumerable<Case> filingQuery,
            string caseNumber)
        {
            if (!string.IsNullOrEmpty(caseNumber))
                filingQuery = filingQuery.Where(p => p.Caption.ToLower().Contains(caseNumber.ToLower()));

            return filingQuery;
        }

        public static IEnumerable<Case> WithCourtNameLike(this IEnumerable<Case> filingQuery,
            string courtName)
        {
            if (!string.IsNullOrEmpty(courtName))
                filingQuery = filingQuery.Where(p => p.Court.Name.ToLower().Contains(courtName.ToLower()));

            return filingQuery;
        }

        public static IEnumerable<Case> WithJudgeLike(this IEnumerable<Case> filingQuery,
            string judge)
        {
            if (!string.IsNullOrEmpty(judge))
                filingQuery = filingQuery.Where(p => p.Judge.ToLower().Contains(judge.ToLower()));

            return filingQuery;
        }

        public static IEnumerable<Case> WithPaging(this IEnumerable<Case> filingQuery,
                                            int? startRow,
                                            int? rowCount)
        {
            if ((!startRow.HasValue) && (!rowCount.HasValue || rowCount.Value == 0))
                return filingQuery;

            return filingQuery.Skip((int)startRow).Take((int)rowCount);
        }

    }
}
