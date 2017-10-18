using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CaseTracker.Portal.Helpers
{
    public static class OrderExt
    {
        //public static IOrderedQueryable<T> Order<T>(this IQueryable<T> source, string propertyName, SortDirection descending, bool anotherLevel = false)
        //{
        //    var param = Expression.Parameter(typeof(T), string.Empty);
        //    if (string.IsNullOrWhiteSpace(propertyName))
        //    {
        //        propertyName = "Id";
        //    }
        //    var property = Expression.PropertyOrField(param, propertyName);
        //    var sort = Expression.Lambda(property, param);

        //    var call = Expression.Call(
        //        typeof(Queryable),
        //        (!anotherLevel ? "OrderBy" : "ThenBy") +
        //        (descending == SortDirection.Descending ? "Descending" : string.Empty),
        //        new[] { typeof(T), property.Type },
        //        source.Expression,
        //        Expression.Quote(sort));

        //    return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        //}

        public static IEnumerable<T> OrderBy2<T>(this IEnumerable<T> list, string sortExpression)
        {
            sortExpression += "";
            var parts = sortExpression.Split(' ');
            var descending = false;
            var property = "";

            if (parts.Length <= 0 || parts[0] == "") return list;

            property = parts[0];

            if (parts.Length > 1)
            {
                @descending = parts[1].ToLower().Contains("esc");
            }

            var prop = typeof(T).GetProperty(property);

            if (prop == null)
            {
                throw new Exception("No property '" + property + "' in + " + typeof(T).Name + "'");
            }

            return @descending ? list.OrderByDescending(x => prop.GetValue(x, null)) : list.OrderBy(x => prop.GetValue(x, null));
        }
    }

    public enum SortDirection
    {
        Descending,
        Ascending
    }
}