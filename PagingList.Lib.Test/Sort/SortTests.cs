using PagingList.Lib.Exceptions;
using PagingList.Lib.Extensions;
using PagingList.Lib.Model;
using PagingList.Lib.Test.Model;

namespace PagingList.Lib.Test.Sort;

public class SortTests
{
    private static List<DataModel> SortableList = new List<DataModel>
    {
        new DataModel(1, 1, "acb", true),
        new DataModel(1, 1, "abc", true),
        new DataModel(1, 1, "cba", true),
    };

    [Fact]
    public void PagedQueryWithSortByStringAscending_ValidOrder()
    {
        var query = new PagedQuery()
        {
            Page = 1,
            ResultsPerPage = 10,
            OrderBy = new[] { "StringProperty" }
        };
        var result = SortableList.AsQueryable().Paginate(query);

        Assert.Equal(SortableList
                .OrderBy(d => d.StringProperty)
                .First(),
            result.Items.First());
    }

    [Fact]
    public void PagedQueryWithSortByStringDescending_ValidOrder()
    {
        var query = new PagedQuery()
        {
            Page = 1,
            ResultsPerPage = 10,
            OrderBy = new[] { "StringProperty", "desc" }
        };
        var list = new List<DataModel>
        {
            new DataModel(1, 1, "acb", true),
            new DataModel(1, 1, "abc", true),
            new DataModel(1, 1, "cba", true),
        };
        var result = SortableList.AsQueryable().Paginate(query);

        Assert.Equal(SortableList
                .OrderByDescending(d => d.StringProperty)
                .First(),
            result.Items.First());
    }

    [Fact]
    public void PagedQueryWithInvalidPropertyName_PropertyNotFoundRaised()
    {
        var query = new PagedQuery()
        {
            Page = 1,
            ResultsPerPage = 10,
            OrderBy = new[] { "NotProperty", "desc" }
        };
        var list = new List<DataModel>
        {
            new DataModel(1, 1, "acb", true),
            new DataModel(1, 1, "abc", true),
            new DataModel(1, 1, "cba", true),
        };
        Assert.Throws<PropertyNotFoundException>(() => SortableList.AsQueryable().Paginate(query));
    }
}