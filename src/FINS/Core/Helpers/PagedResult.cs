using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FINS.Core.Helpers
{
    public class PagedResult<T>
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; private set; }

        public long TotalRecordCount { get; set; }

        public PagedResult(IEnumerable<T> items, int pageNo, int pageSize, long totalRecordCount)
        {
            Items = new List<T>(items);

            PageNo = pageNo;
            PageSize = pageSize;
            TotalRecordCount = totalRecordCount;

            TotalPages = totalRecordCount > 0
                        ? (int)Math.Ceiling(totalRecordCount / (double)PageSize)
                        : 0;
        }

        public List<T> Items { get; set; }
    }
}
