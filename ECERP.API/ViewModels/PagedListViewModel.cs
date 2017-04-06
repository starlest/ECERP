namespace ECERP.API.ViewModels
{
    using System.Collections.Generic;

    public class PagedListViewModel<T>
    {
        public IList<T> Source { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}