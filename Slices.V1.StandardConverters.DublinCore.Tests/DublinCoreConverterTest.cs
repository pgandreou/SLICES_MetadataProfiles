using Slices.TestsSupport;
using Slices.V1.Format;

namespace Slices.V1.StandardConverters.DublinCore.Tests;

public class DublinCoreConverterTest
{
    [Fact]
    public void FromExtrenal1()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\immunarch-0-6-9.xml");

        DublinCoreSerializer serializer = new();
        SlicesDublinCoreConverter converter = new(serializer);

        DublinCoreObject dublinCoreObject = serializer.FromXml(textReader);
        DigitalObject slicesObject = converter.FromExtrenal(dublinCoreObject);

        Assert.NotNull(slicesObject);
    }
}
