using PagingList.Lib.Extensions;
using PagingList.Lib.Model;
using PagingList.Lib.Test.Model;
using PagingList.Lib.Test.TestInputs;

namespace PagingList.Lib.Test.Paging;

public class PaginateTests
{
    [Theory]
    [ClassData(typeof(PagedQueryTestInput))]
    public void ValidPagedQueryAndEmptyQueryable_ValidEmptyResults(PagedQuery query, int expectedCount)
    {
        var result = DataModel.Data.AsQueryable().Paginate(query);
        Assert.Equal(expectedCount, result.Items.Count());
        Assert.Equal(query.Page, result.Page);
        Assert.Equal(query.ResultsPerPage, result.ResultsPerPage);
    }
}