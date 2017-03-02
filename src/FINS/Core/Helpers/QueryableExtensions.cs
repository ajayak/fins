using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using FINS.Core.FinsExceptions;
using FINS.Models;

namespace FINS.Core.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sort)
        {
            if (source == null)
            {
                throw new FinsInvalidDataException("source");
            }

            if (string.IsNullOrEmpty(sort) ||
                string.IsNullOrWhiteSpace(sort))
            {
                return source.OrderBy("id");
            }

            // split the sort string
            var lstSort = sort.Split(',');

            // run through the sorting options and apply them - in reverse
            // order, otherwise results will come out sorted by the last 
            // item in the string first!
            foreach (var sortOption in lstSort.Reverse())
            {
                // if the sort option starts with "-", we order
                // descending, ortherwise ascending

                if (sortOption.StartsWith("-"))
                {
                    source = source.OrderBy(sortOption.Remove(0, 1) + " descending");
                }
                else
                {
                    source = source.OrderBy(sortOption);
                }
            }

            return source;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> source, int pageNo = 1, int pageSize = 10)
        {
            var maxPageSize = 110;
            if (source == null)
            {
                throw new FinsInvalidDataException("source");
            }

            if (pageNo < 1)
            {
                pageNo = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            return source
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize < maxPageSize ? pageSize : maxPageSize);
        }
    }
}
