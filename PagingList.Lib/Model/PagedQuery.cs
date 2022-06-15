namespace PagingList.Lib.Model;

public class PagedQuery
{
    public int Page { get; set; }
    public List<string[]> FilterBy { get; set; }
    public string[] OrderBy { get; set; }
    public int ResultsPerPage { get; set; }
}