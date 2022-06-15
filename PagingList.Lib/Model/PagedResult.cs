namespace PagingList.Lib.Model;

public class PagedResult<T>
{
    public IQueryable<T> Items { get; set; }
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public int ResultsPerPage { get; set; }
}