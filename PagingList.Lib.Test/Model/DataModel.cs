namespace PagingList.Lib.Test.Model;

public class DataModel
{
    public string StringProperty { get; set; } = string.Empty;
    public int IntProperty { get; set; }
    public double DoubleProperty { get; set; }
    public bool BoolProperty { get; set; }

    public DataModel(int intProperty, double doubleProperty, string stringProperty, bool boolProperty)
    {
        IntProperty = intProperty;
        DoubleProperty = doubleProperty;
        StringProperty = stringProperty;
        BoolProperty = boolProperty;
    }

    public static List<DataModel> Data => new()
    {
        new DataModel(10, 10.5, "test", true),
        new DataModel(100, 10.5, "test", true),
        new DataModel(10, 100.5, "starting not test", false),
        new DataModel(10, 10.5, "test with spaces", false),
        new DataModel(10, 10.5, "without test", false)
    };
}