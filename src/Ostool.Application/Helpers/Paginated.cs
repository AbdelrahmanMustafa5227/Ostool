using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Helpers
{
    public class Paginated<T> where T : class
    {
        private int TotalCount;

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasNextPage => PageIndex * PageSize < TotalCount;
        public bool HasPrevPage => PageIndex > 1;
        public List<T> Items { get; set; }


        private Paginated(List<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public static Paginated<T> Create(List<T> items, int pageNumber, int pageSize, int totalCount)
        {
            return new Paginated<T>(items, pageNumber, pageSize, totalCount);
        }

        public static Paginated<T> CreateEmpty()
        {
            return new Paginated<T>([], 1, 5, 0);
        }
    }
}