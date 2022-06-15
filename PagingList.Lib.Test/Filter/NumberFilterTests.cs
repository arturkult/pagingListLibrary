using PagingList.Lib.Exceptions;
using PagingList.Lib.Extensions;
using PagingList.Lib.Model;
using PagingList.Lib.Test.Model;
using PagingList.Lib.Test.TestInputs;

namespace PagingList.Lib.Test.Filter;

public class NumberFilterTests
{
    [Theory]
    [ClassData(typeof(NumberOperatorTestInput))]
    public void ValidPagedQueryWithNumber_ValidResults(string @operator)
    {
        var query = new PagedQuery
        {
            Page = 1,
            FilterBy = new List<string[]> { new[] { "IntProperty", @operator, "10" } },
            ResultsPerPage = 10
        };
        var list = DataModel.Data;
        var result = list.AsQueryable().Paginate(query);
        var expectedResult = list
            .Where(NumberOperatorTestInput.OperatorActions[@operator](10))
            .ToList();
        Assert.Equal(expectedResult.Count, result.TotalCount);
    }

    [Fact]
    public void PagedQueryInvalidOperator_NotSupportedOperatorRaised()
    {
        var query = new PagedQuery
        {
            Page = 1,
            FilterBy = new List<string[]> { new[] { "IntProperty", "test", "10" } },
            ResultsPerPage = 10
        };
        var list = DataModel.Data;
        Assert.Throws<FilterOperatorNotSupportedException>(() => list.AsQueryable().Paginate(query));
    }
}