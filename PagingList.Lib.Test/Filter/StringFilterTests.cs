using PagingList.Lib.Exceptions;
using PagingList.Lib.Extensions;
using PagingList.Lib.Model;
using PagingList.Lib.Test.Model;
using PagingList.Lib.Test.TestInputs;

namespace PagingList.Lib.Test.Filter;

public class StringFilterTests
{
    [Theory]
    [ClassData(typeof(StringOperatorTestInput))]
    public void ValidPagedQueryWithString_ValidResults(string @operator)
    {
        var query = new PagedQuery
        {
            Page = 1,
            FilterBy = new List<string[]> { new[] { "StringProperty", @operator, "test" } },
            ResultsPerPage = 10
        };
        var list = DataModel.Data;
        var result = list.AsQueryable().Paginate(query);
        var expectedResult = list
            .Where(StringOperatorTestInput.OperatorActions[@operator]("test"))
            .ToList();
        Assert.Equal(expectedResult.Count, result.TotalCount);
    }

    [Fact]
    public void PagedQueryInvalidOperator_NotSupportedOperatorRaised()
    {
        var query = new PagedQuery
        {
            Page = 1,
            FilterBy = new List<string[]> { new[] { "StringProperty", "test", "test" } },
            ResultsPerPage = 10
        };
        var list = DataModel.Data;
        Assert.Throws<FilterOperatorNotSupportedException>(() => list.AsQueryable().Paginate(query));
    }
}