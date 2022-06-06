using Slices.TestsSupport;
using Slices.V1.Standard;

namespace Slices.V1.StandardConverters.DataCite.Tests;

public class DataCiteConverterTest
{
    [Fact]
    public void Test1()
    {
        using StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\datacite-example-full-v4.xml");
        DataCiteConverter converter = new(new DataCiteSerializer());

        DigitalObject slicesObject = converter.FromSerializedExtrenal(reader, null);

        Assert.NotNull(slicesObject);
    }
}