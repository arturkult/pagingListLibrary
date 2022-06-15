using PagingList.Lib.Exceptions;
using PagingList.Lib.Extensions;
using PagingList.Lib.Model;
using PagingList.Lib.Test.Model;

namespace PagingList.Lib.Test.Filter;

public class UnsupportedTypeTest
{
    [Fact]
    public void PagedQueryWithInvalidFilterType_UnsupportedTypeRaised()
    {
        var query = new PagedQuery
        {
            Page = 1,
            FilterBy = new List<string[]> { new[] { "BoolProperty", "=", "true" } },
            ResultsPerPage = 10
        };
        var list = DataModel.Data;

        Assert.Throws<TypeNotSupportedException>(() => list.AsQueryable().Paginate(query));
    }
    
    [Fact]
    public void PagedQueryWithInvalidProperty_PropertyNotFoundRaised()
    {
        var query = new PagedQuery
        {
            Page = 1,
            FilterBy = new List<string[]> { new[] { "Abc", "=", "10" } },
            ResultsPerPage = 10
        };
        var list = DataModel.Data;

        Assert.Throws<PropertyNotFoundException>(() => list.AsQueryable().Paginate(query));
    }
}