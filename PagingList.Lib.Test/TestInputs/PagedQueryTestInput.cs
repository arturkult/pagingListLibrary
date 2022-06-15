using System.Collections;
using PagingList.Lib.Model;
using PagingList.Lib.Test.Model;

namespace PagingList.Lib.Test.TestInputs;

public class PagedQueryTestInput: IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new PagedQuery() { Page = 1, ResultsPerPage = 10 }, Math.Min(DataModel.Data.Count, 10) };
        yield return new object[] { new PagedQuery() { Page = 2, ResultsPerPage = 10 }, 0 };
        yield return new object[] { new PagedQuery() { Page = 1, ResultsPerPage = 0 }, 0 };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}