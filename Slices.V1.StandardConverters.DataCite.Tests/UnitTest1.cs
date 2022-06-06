using Slices.TestsSupport;
using Slices.V1.StandardConverters.DataCite.Model;
using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\datacite-example-full-v4.xml");
        XmlSerializer xmlSerializer = new(typeof(DataCiteResource));

        DataCiteResource? resource = (DataCiteResource?)xmlSerializer.Deserialize(reader);

        Assert.NotNull(resource);
    }
}