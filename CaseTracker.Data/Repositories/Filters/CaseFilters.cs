using CaseTracker.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace CaseTracker.Data.Repositories
{
    public static class CaseFilters
    {
        public static IEnumerable<Case> WithCaptionLike(this IEnumerable<Case> caseQuery,
                                               string caption)
        {
            if (!string.IsNullOrEmpty(caption))
                caseQuery = caseQuery.Where(p => !string.IsNullOrWhiteSpace(p.Caption) && p.Caption.ToLower().Contains(caption.ToLower()));

            return caseQuery;
        }

        public static IEnumerable<Case> WithCaseNumberLike(this IEnumerable<Case> caseQuery,
            string caseNumber)
        {
            if (!string.IsNullOrEmpty(caseNumber))
                caseQuery = caseQuery.Where(p => !string.IsNullOrWhiteSpace(p.CaseNumber) && p.CaseNumber.ToLower().Contains(caseNumber.ToLower()));

            return caseQuery;
        }

        public static IEnumerable<Case> WithCourtNameLike(this IEnumerable<Case> caseQuery,
            string courtName)
        {
            if (!string.IsNullOrEmpty(courtName))
                caseQuery = caseQuery.Where(p => p.Court.Name.ToLower().Contains(courtName.ToLower()));

            return caseQuery;
        }

        public static IEnumerable<Case> WithJudgeLike(this IEnumerable<Case> caseQuery,
            string judge)
        {
            if (!string.IsNullOrEmpty(judge))
                caseQuery = caseQuery.Where(p => !string.IsNullOrWhiteSpace(p.Judge) && p.Judge.ToLower().Contains(judge.ToLower()));

            return caseQuery;
        }

        public static IEnumerable<Case> WithJurisdictionLike(this IEnumerable<Case> caseQuery,
            string jurisdiction)
        {
            if (!string.IsNullOrEmpty(jurisdiction))
                caseQuery = caseQuery.Where(p => p.Court.Jurisdiction.Name.ToLower().Contains(jurisdiction.ToLower()));

            return caseQuery;
        }

        public static IEnumerable<Case> WithPaging(this IEnumerable<Case> caseQuery,
                                            int? startRow,
                                            int? rowCount)
        {
            if ((!startRow.HasValue) && (!rowCount.HasValue || rowCount.Value == 0))
                return caseQuery;

            return caseQuery.Skip((int)startRow).Take((int)rowCount);
        }

    }
}
