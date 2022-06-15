using System.Collections;
using PagingList.Lib.Test.Model;

namespace PagingList.Lib.Test.TestInputs;

public class NumberOperatorTestInput: IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "<" };
        yield return new object[] { ">" };
        yield return new object[] { "<=" };
        yield return new object[] { ">=" };
        yield return new object[] { "=" };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static Dictionary<string, Func<int, Func<DataModel, bool>>> OperatorActions =>
        new()
        {
            { "<", value => (model => model.IntProperty < value) },
            { ">", value => (model => model.IntProperty > value) },
            { "<=", value => (model => model.IntProperty <= value) },
            { ">=", value => (model => model.IntProperty >= value) },
            { "=", value => (model => model.IntProperty == value) }
        };
}