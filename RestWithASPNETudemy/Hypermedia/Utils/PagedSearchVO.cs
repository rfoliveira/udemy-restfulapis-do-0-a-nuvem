using System.Collections.Generic;
using RestWithASPNETUdemy.Hypermedia.Abstract;

namespace RestWithASPNETUdemy.Hypermedia.Utils
{
    public class PagedSearchVO<T> where T: ISupportHypermedia
    {
        private const int DEFAULT_PAGESiZE = 10;

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortFields { get; set; }
        public string SortDirection { get; set; }
        public Dictionary<string, object> Filters { get; set; }
        public List<T> List { get; set; }

        public PagedSearchVO()
        {
        }

        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirection)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirection = sortDirection;
        }

        public PagedSearchVO(int currentPage, int pageSize, 
            string sortFields, string sortDirection, 
            Dictionary<string, object> filters) : this(currentPage, pageSize, sortFields, sortDirection)
        {
            Filters = filters;
        }

        public PagedSearchVO(int currentPage, string sortFields, string sortDirection)
            : this(currentPage, DEFAULT_PAGESiZE, sortFields, sortDirection)
        {
        }

        public int GetCurrentPage() => CurrentPage == 0 ? 2 : CurrentPage;
        public int GetPageSize() => PageSize == 0 ? DEFAULT_PAGESiZE : PageSize; 
    }
}