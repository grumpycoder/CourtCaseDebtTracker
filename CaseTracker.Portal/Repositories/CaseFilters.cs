using CaseTracker.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace CaseTracker.Portal.Repositories
{
    public static class CaseFilters
    {
        public static IEnumerable<Filing> WithCaptionLike(this IEnumerable<Filing> filingQuery,
                                               string caption)
        {
            if (!string.IsNullOrEmpty(caption))
                filingQuery = filingQuery.Where(p => p.Caption.ToLower().Contains(caption.ToLower()));

            return filingQuery;
        }

        public static IEnumerable<Filing> WithCaseNumberLike(this IEnumerable<Filing> filingQuery,
            string caseNumber)
        {
            if (!string.IsNullOrEmpty(caseNumber))
                filingQuery = filingQuery.Where(p => p.Caption.ToLower().Contains(caseNumber.ToLower()));

            return filingQuery;
        }

        public static IEnumerable<Filing> WithCourtNameLike(this IEnumerable<Filing> filingQuery,
            string courtName)
        {
            if (!string.IsNullOrEmpty(courtName))
                filingQuery = filingQuery.Where(p => p.Court.Name.ToLower().Contains(courtName.ToLower()));

            return filingQuery;
        }

        public static IEnumerable<Filing> WithJudgeLike(this IEnumerable<Filing> filingQuery,
            string judge)
        {
            if (!string.IsNullOrEmpty(judge))
                filingQuery = filingQuery.Where(p => p.Judge.ToLower().Contains(judge.ToLower()));

            return filingQuery;
        }

        public static IEnumerable<Filing> WithPaging(this IEnumerable<Filing> filingQuery,
                                            int? startRow,
                                            int? rowCount)
        {
            if ((!startRow.HasValue) && (!rowCount.HasValue || rowCount.Value == 0))
                return filingQuery;

            return filingQuery.Skip((int)startRow).Take((int)rowCount);
        }

    }
}
