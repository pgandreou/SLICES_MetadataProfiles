using Slices.TestsSupport;
using Slices.V1.StandardConverters.DataCite.Model;

namespace Slices.V1.StandardConverters.DataCite.Tests;

public class DataCiteSerializerTest
{
    [Fact]
    public void Test1()
    {
        using StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\datacite-example-full-v4.xml");
        DataCiteSerializer serializer = new();

        DataCiteResource resource = serializer.FromXml(reader);

        Assert.NotNull(resource);
    }
}