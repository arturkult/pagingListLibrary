using System.Collections;
using PagingList.Lib.Test.Model;

namespace PagingList.Lib.Test.TestInputs;

public class StringOperatorTestInput: IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "contains" };
        yield return new object[] { "startsWith" };
        yield return new object[] { "endsWith" };
        yield return new object[] { "=" };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static Dictionary<string, Func<string, Func<DataModel, bool>>> OperatorActions =>
        new()
        {
            { "contains", value => (model => model.StringProperty.Contains(value)) },
            { "startsWith", value => (model => model.StringProperty.StartsWith(value)) },
            { "endsWith", value => (model => model.StringProperty.EndsWith(value)) },
            { "=", value => (model => model.StringProperty.Equals(value)) }
        };
}