using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanWebApi.Core.CustomEntities
{
    public class PagedList<T> : List<T>
    {   //paginacion en los enumerables
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?) null;
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalPages = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count();
            List<T> items = source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList(); //skipea N registros y toma E registros, paginacion
            return new PagedList<T>(items,count,pageNumber,pageSize);
        }
    }
}
