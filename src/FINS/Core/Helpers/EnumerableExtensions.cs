using System.Collections.Generic;

namespace FINS.Core.Helpers
{
    public static class EnumerableExtensions
    {
        public static PagedResult<T> ToPagedResult<T>
            (this IEnumerable<T> source, int pageNo, int pageSize, int totalRecordCount)
        {
            return new PagedResult<T>(source, pageNo, pageSize, totalRecordCount);
        }
    }
}
